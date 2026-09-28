using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;

namespace Todoist.Net.OAuth
{
    /// <summary>
    /// Authorizes requests with OAuth tokens and refreshes the tokens when they expire or get rejected.
    /// </summary>
    /// <remarks>
    /// Every instance holds the tokens of a single user, so it must never be shared between clients
    /// the way <c>IHttpClientFactory</c> shares the handlers it pools.
    /// </remarks>
    internal sealed class TodoistOAuthHandler : DelegatingHandler
    {
        // Refreshing slightly ahead of the expiration keeps a request from carrying a token which expires in transit.
        private static readonly TimeSpan ExpirationMargin = TimeSpan.FromMinutes(1);

        private readonly SemaphoreSlim _refreshLock = new SemaphoreSlim(1, 1);

        private readonly Func<TodoistTokens, Task> _onTokensRefreshed;

        private readonly TodoistOAuthOptions _options;

        // Every access goes through Volatile.Read/Volatile.Write: the request path reads the tokens without
        // taking _refreshLock, while a refresh or a revocation swaps them under the lock, so each access
        // needs explicit acquire/release semantics to establish the happens-before edge between them.
        private TodoistTokens _tokens;

        public TodoistOAuthHandler(
            TodoistTokens tokens,
            TodoistOAuthOptions options = null,
            Func<TodoistTokens, Task> onTokensRefreshed = null)
        {
            ThrowHelper.ThrowIfNull(tokens, nameof(tokens));
            if (!string.IsNullOrEmpty(tokens.RefreshToken))
            {
                ThrowHelper.ThrowIfNull(
                    onTokensRefreshed,
                    nameof(onTokensRefreshed),
                    "A callback must be provided to handle refreshed tokens when using a refresh token.");

                ThrowHelper.ThrowIfNull(
                    options, 
                    nameof(options), 
                    "Todoist Client ID and Client Secret must be provided when using a refresh token.");

                ThrowHelper.ThrowIfNullOrEmpty(
                    options.ClientId,
                    nameof(options.ClientId),
                    "Todoist Client ID must be provided when using a refresh token.");

                ThrowHelper.ThrowIfNullOrEmpty(
                    options.ClientSecret,
                    nameof(options.ClientSecret),
                    "Todoist Client Secret must be provided when using a refresh token.");
            }

            _onTokensRefreshed = onTokensRefreshed;
            _options = options;
            _tokens = tokens;
        }

        /// <summary>
        /// Gets or sets how long a request to the token or revocation endpoint may take, including reading its response.
        /// </summary>
        /// <remarks>
        /// These requests are sent past the <see cref="HttpClient" />, so its timeout does not apply to them.
        /// </remarks>
        public TimeSpan TokenRequestTimeout { get; set; } = TimeSpan.FromSeconds(100);

        public TodoistTokens Tokens => Volatile.Read(ref _tokens);

        public Task<TodoistTokens> RefreshTokensAsync(CancellationToken cancellationToken = default)
        {
            var tokens = Volatile.Read(ref _tokens);
            if (!CanRefresh(tokens))
            {
                throw new InvalidOperationException(
                    "The tokens cannot be refreshed without a refresh token.");
            }

            return SafelyRefreshTokensAsync(tokens, cancellationToken);
        }

        public Task RevokeTokensAsync(CancellationToken cancellationToken = default)
        {
            if (!CanRevoke(_options))
            {
                throw new InvalidOperationException(
                    "Revoking the tokens requires the client ID and client secret of the application.");
            }

            return SafelyRevokeTokensAsync(cancellationToken);
        }


        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var tokens = Volatile.Read(ref _tokens);
            if (CanRefresh(tokens) && IsExpiring(tokens))
            {
                tokens = await SafelyRefreshTokensAsync(tokens, cancellationToken)
                    .ConfigureAwait(false);
            }

            var response = await SendAuthorizedAsync(request, tokens, cancellationToken)
                .ConfigureAwait(false);
            if (response.StatusCode != HttpStatusCode.Unauthorized || !CanRefresh(tokens) || !IsReplayable(request))
            {
                return response;
            }

            // The access token got rejected ahead of its known expiration, or its expiration is unknown.
            response.Dispose();
            tokens = await SafelyRefreshTokensAsync(tokens, cancellationToken)
                .ConfigureAwait(false);

            return await SendAuthorizedAsync(request, tokens, cancellationToken)
                .ConfigureAwait(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _refreshLock?.Dispose();
            }

            base.Dispose(disposing);
        }


        private Task<HttpResponseMessage> SendAuthorizedAsync(
            HttpRequestMessage request,
            TodoistTokens tokens,
            CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

            return base.SendAsync(request, cancellationToken);
        }

        private async Task<TodoistTokens> SafelyRefreshTokensAsync(TodoistTokens staleTokens, CancellationToken cancellationToken)
        {
            // Waiting honors the cancellation of each caller, while the refresh itself cannot be canceled:
            // abandoning it after Todoist rotated the refresh token would lose the new one.
            await _refreshLock.WaitAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var currentTokens = Volatile.Read(ref _tokens);
                if (!ReferenceEquals(currentTokens, staleTokens))
                {
                    // Another request refreshed the tokens while this one was waiting.
                    return currentTokens;
                }
                TokenResponse tokenResponse;

                // Create a timeout source for the token request. The cancellation token is set to None
                // because we want the refresh operation to complete even if the original request is canceled.
                using (var timeoutSource = CreateTokenRequestTimeoutSource(CancellationToken.None))
                using (var request = ApiRequestBuilder.BuildRefreshTokensRequest(_options.ClientId, _options.ClientSecret, currentTokens.RefreshToken))
                {
                    // The timeout keeps running while the response is read, since a body can stall after the headers arrived.
                    tokenResponse = await TodoistSerializer
                        .ProcessRequestAsync<TokenResponse>(ct => base.SendAsync(request, ct), timeoutSource.Token)
                        .ConfigureAwait(false);
                }
                var expiresAt = tokenResponse.ExpiresIn > 0
                    ? DateTimeOffset.UtcNow.AddSeconds(tokenResponse.ExpiresIn)
                    : (DateTimeOffset?)null;

                var refreshedTokens = new TodoistTokens(tokenResponse.AccessToken, tokenResponse.RefreshToken, expiresAt);

                // The tokens are swapped before they are reported, so a failure to store them does not break this client.
                Volatile.Write(ref _tokens, refreshedTokens);
                
                if (!string.IsNullOrEmpty(refreshedTokens.RefreshToken))
                {
                    await _onTokensRefreshed(refreshedTokens)
                        .ConfigureAwait(false);
                }
                return refreshedTokens;
            }
            finally
            {
                _refreshLock.Release();
            }
        }
        
        private async Task SafelyRevokeTokensAsync(CancellationToken cancellationToken)
        {
            // Holding the refresh lock keeps a refresh in progress from bringing the revoked access back to life.
            await _refreshLock.WaitAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var tokens = Volatile.Read(ref _tokens);

                using (var timeoutSource = CreateTokenRequestTimeoutSource(cancellationToken))
                using (var request = ApiRequestBuilder.BuildRevokeTokenRequest(_options.ClientId, _options.ClientSecret, tokens.AccessToken))
                {
                    await TodoistSerializer
                        .ProcessRequestAsync(ct => base.SendAsync(request, ct), timeoutSource.Token)
                        .ConfigureAwait(false);
                }

                // Todoist revokes access tokens only, and rejects refresh tokens with 403, so the refresh token
                // keeps working elsewhere. Dropping it keeps at least this client from refreshing the access back to life.
                var accessOnlyTokens = new TodoistTokens(tokens.AccessToken, expiresAt: tokens.ExpiresAt);
                Volatile.Write(ref _tokens, accessOnlyTokens);
            }
            finally
            {
                _refreshLock.Release();
            }
        }

        private CancellationTokenSource CreateTokenRequestTimeoutSource(CancellationToken cancellationToken)
        {
            var timeoutSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeoutSource.CancelAfter(TokenRequestTimeout);

            return timeoutSource;
        }


        private static bool CanRevoke(TodoistOAuthOptions options)
        {
            return !string.IsNullOrEmpty(options?.ClientId) 
                && !string.IsNullOrEmpty(options?.ClientSecret);
        }

        private static bool CanRefresh(TodoistTokens tokens)
        {
            return !string.IsNullOrEmpty(tokens.RefreshToken);
        }

        private static bool IsExpiring(TodoistTokens tokens)
        {
            return tokens.ExpiresAt.HasValue && tokens.ExpiresAt.Value - ExpirationMargin <= DateTimeOffset.UtcNow;
        }

        private static bool IsReplayable(HttpRequestMessage request)
        {
            return !request.Properties.ContainsKey(ApiRequestBuilder.NonReplayableRequestKey);
        }
    }
}
