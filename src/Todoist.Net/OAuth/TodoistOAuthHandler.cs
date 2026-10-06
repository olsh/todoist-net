using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
        /// <summary>
        /// The key of the <see cref="HttpRequestMessage.Properties" /> entry which marks a request
        /// whose content can only be sent once, so it must not be sent again with refreshed tokens.
        /// </summary>
        internal const string NonReplayableRequestKey = "Todoist.Net.NonReplayableRequest";

        private static readonly Uri TokenEndpoint = new Uri("https://api.todoist.com/oauth/access_token");

        private static readonly Uri RevokeEndpoint = new Uri("https://api.todoist.com/api/v1/revoke");

        // Refreshing slightly ahead of the expiration keeps a request from carrying a token which expires in transit.
        private static readonly TimeSpan ExpirationMargin = TimeSpan.FromMinutes(1);

        private readonly SemaphoreSlim _refreshLock = new SemaphoreSlim(1, 1);

        private readonly string _clientId;

        private readonly string _clientSecret;

        private readonly Func<TodoistTokens, Task> _onTokensRefreshed;

        private volatile TodoistTokens _tokens;

        public TodoistOAuthHandler(
            TodoistOAuthOptions options,
            TodoistTokens tokens,
            Func<TodoistTokens, Task> onTokensRefreshed)
        {
            ThrowHelper.ThrowIfNull(options, nameof(options));
            ThrowHelper.ThrowIfNullOrEmpty(options.ClientId, nameof(options));
            ThrowHelper.ThrowIfNull(tokens, nameof(tokens));
            if (!string.IsNullOrEmpty(tokens.RefreshToken) && onTokensRefreshed == null)
            {
                throw new ArgumentNullException(
                    nameof(onTokensRefreshed),
                    "Todoist rotates the refresh token on every refresh, so refreshed tokens have to be stored to keep the access.");
            }

            _clientId = options.ClientId;
            _clientSecret = options.ClientSecret;
            _tokens = tokens;
            _onTokensRefreshed = onTokensRefreshed;
        }

        public TodoistTokens Tokens => _tokens;

        /// <summary>
        /// Gets or sets how long a request to the token or revocation endpoint may take, including reading its response.
        /// </summary>
        /// <remarks>
        /// These requests are sent past the <see cref="HttpClient" />, so its timeout does not apply to them.
        /// </remarks>
        internal TimeSpan TokenRequestTimeout { get; set; } = TimeSpan.FromSeconds(100);

        public Task<TodoistTokens> RefreshTokensAsync(CancellationToken cancellationToken)
        {
            var tokens = _tokens;
            if (!CanRefresh(tokens))
            {
                throw new InvalidOperationException("The tokens cannot be refreshed without a refresh token.");
            }

            return RefreshAsync(tokens, cancellationToken);
        }

        public async Task RevokeTokensAsync(CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(_clientSecret))
            {
                throw new InvalidOperationException(
                    "Revoking the tokens requires the client secret of the application.");
            }

            // Holding the refresh lock keeps a refresh in progress from bringing the revoked access back to life.
            await _refreshLock.WaitAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var tokens = _tokens;
                await RevokeAccessTokenAsync(tokens.AccessToken, cancellationToken)
                    .ConfigureAwait(false);

                // Todoist revokes access tokens only, and rejects refresh tokens with 403, so the refresh token
                // keeps working elsewhere. Dropping it keeps at least this client from refreshing the access back to life.
                _tokens = new TodoistTokens(tokens.AccessToken, expiresAt: tokens.ExpiresAt);
            }
            finally
            {
                _refreshLock.Release();
            }
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var tokens = _tokens;
            if (CanRefresh(tokens) && IsExpiring(tokens))
            {
                tokens = await RefreshAsync(tokens, cancellationToken)
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
            tokens = await RefreshAsync(tokens, cancellationToken)
                .ConfigureAwait(false);

            return await SendAuthorizedAsync(request, tokens, cancellationToken)
                .ConfigureAwait(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _refreshLock.Dispose();
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

        private async Task<TodoistTokens> RefreshAsync(TodoistTokens staleTokens, CancellationToken cancellationToken)
        {
            // Waiting honors the cancellation of each caller, while the refresh itself cannot be canceled:
            // abandoning it after Todoist rotated the refresh token would lose the new one.
            await _refreshLock.WaitAsync(cancellationToken)
                .ConfigureAwait(false);
            try
            {
                var currentTokens = _tokens;
                if (!ReferenceEquals(currentTokens, staleTokens))
                {
                    // Another request refreshed the tokens while this one was waiting.
                    return currentTokens;
                }

                return await RefreshCoreAsync(currentTokens)
                    .ConfigureAwait(false);
            }
            finally
            {
                _refreshLock.Release();
            }
        }

        private async Task<TodoistTokens> RefreshCoreAsync(TodoistTokens tokens)
        {
            var formParams = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "refresh_token", tokens.RefreshToken },
                { "client_id", _clientId }
            };
            if (!string.IsNullOrEmpty(_clientSecret))
            {
                formParams.Add("client_secret", _clientSecret);
            }

            TokenResponse tokenResponse;
            using (var timeoutSource = CreateTokenRequestTimeoutSource(CancellationToken.None))
            using (var request = new HttpRequestMessage(HttpMethod.Post, TokenEndpoint))
            {
                request.Content = new FormUrlEncodedContent(formParams);

                // The timeout keeps running while the response is read, since a body can stall after the headers arrived.
                using (var response = await base.SendAsync(request, timeoutSource.Token)
                           .ConfigureAwait(false))
                {
                    await TodoistClient.EnsureSuccessResponseAsync(response, timeoutSource.Token)
                        .ConfigureAwait(false);

                    tokenResponse = await TodoistClient
                        .DeserializeResponseAsync<TokenResponse>(response, timeoutSource.Token)
                        .ConfigureAwait(false);
                }
            }

            var expiresAt = tokenResponse.ExpiresIn > 0
                ? DateTimeOffset.UtcNow.AddSeconds(tokenResponse.ExpiresIn)
                : (DateTimeOffset?)null;

            if (string.IsNullOrEmpty(tokenResponse.RefreshToken))
            {
                // Todoist answers a refresh token used within the last minute with the access token it issued the first time,
                // but without a refresh token. Whoever used it first got the new refresh token and has to store it, so these
                // tokens must not be reported for storing, or they would overwrite the only refresh token which still works.
                var accessOnlyTokens = new TodoistTokens(tokenResponse.AccessToken, expiresAt: expiresAt);
                _tokens = accessOnlyTokens;

                return accessOnlyTokens;
            }

            var refreshedTokens = new TodoistTokens(tokenResponse.AccessToken, tokenResponse.RefreshToken, expiresAt);

            // The tokens are swapped before they are reported, so a failure to store them does not break this client.
            _tokens = refreshedTokens;
            await _onTokensRefreshed(refreshedTokens)
                .ConfigureAwait(false);

            return refreshedTokens;
        }

        private async Task RevokeAccessTokenAsync(string accessToken, CancellationToken cancellationToken)
        {
            var formParams = new Dictionary<string, string>
            {
                { "token", accessToken },
                { "token_type_hint", "access_token" }
            };

            using (var timeoutSource = CreateTokenRequestTimeoutSource(cancellationToken))
            using (var request = new HttpRequestMessage(HttpMethod.Post, RevokeEndpoint))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}")));
                request.Content = new FormUrlEncodedContent(formParams);

                using (var response = await base.SendAsync(request, timeoutSource.Token)
                           .ConfigureAwait(false))
                {
                    await TodoistClient.EnsureSuccessResponseAsync(response, timeoutSource.Token)
                        .ConfigureAwait(false);
                }
            }
        }

        private CancellationTokenSource CreateTokenRequestTimeoutSource(CancellationToken cancellationToken)
        {
            var timeoutSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeoutSource.CancelAfter(TokenRequestTimeout);

            return timeoutSource;
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
            return !request.Properties.ContainsKey(NonReplayableRequestKey);
        }
    }
}
