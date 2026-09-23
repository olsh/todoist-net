using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net
{
    internal class RefreshableTodoistRestClient : TodoistRestClient, IRefreshableTodoistRestClient
    {
        private Task<TokenRefreshResponseMessage> _cachedRefreshTask = null;
        private readonly object _refreshLock = new object();

        private readonly TodoistAuthenticationContext _authContext;

        public RefreshableTodoistRestClient(TodoistAuthenticationContext authContext) : base(authContext?.Tokens?.AccessToken)
        {
            ThrowHelper.ThrowIfNull(authContext, nameof(authContext));

            _authContext = authContext;
        }

        public RefreshableTodoistRestClient(TodoistAuthenticationContext authContext, IWebProxy proxy) : base(authContext?.Tokens?.AccessToken, proxy)
        {
            ThrowHelper.ThrowIfNull(authContext, nameof(authContext));

            _authContext = authContext;
        }

        public RefreshableTodoistRestClient(TodoistAuthenticationContext authContext, HttpClient httpClient) : base(authContext?.Tokens?.AccessToken, httpClient)
        {
            ThrowHelper.ThrowIfNull(authContext, nameof(authContext));

            _authContext = authContext;
        }


        /// <inheritdoc/>
        public override Task<HttpResponseMessage> GetAsync(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default)
        {
            return ExecuteWithTokenRefreshAsync(() =>
                base.GetAsync(resource, queryParams, cancellationToken), cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public override Task<HttpResponseMessage> PostAsync(string resource, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default)
        {
            return ExecuteWithTokenRefreshAsync(() =>
                base.PostAsync(resource, formParams, cancellationToken), cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public override Task<HttpResponseMessage> PostFilesAsync(string resource, UploadFile[] files, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default)
        {
            // If any of the files cannot seek, we cannot retry the request after refreshing the token, because the file streams would be at the end.
            bool canRetry = files?.All(f => f.ContentStream.CanSeek) ?? true;

            return ExecuteWithTokenRefreshAsync(() =>
                base.PostFilesAsync(resource, files, formParams, cancellationToken), canRetry, cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public override Task<HttpResponseMessage> PostJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
        {
            return ExecuteWithTokenRefreshAsync(() =>
                base.PostJsonAsync(resource, jsonContent, cancellationToken), cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public override Task<HttpResponseMessage> PutAsync(string resource, CancellationToken cancellationToken = default)
        {
            return ExecuteWithTokenRefreshAsync(() =>
                base.PutAsync(resource, cancellationToken), cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public override Task<HttpResponseMessage> PutJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
        {
            return ExecuteWithTokenRefreshAsync(() =>
                base.PutJsonAsync(resource, jsonContent, cancellationToken), cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public override Task<HttpResponseMessage> DeleteAsync(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default)
        {
            return ExecuteWithTokenRefreshAsync(() => 
                base.DeleteAsync(resource, queryParams, cancellationToken), cancellationToken: cancellationToken);
        }


        /// <inheritdoc/>
        public Task<TokenRefreshResponseMessage> RefreshTokensAsync(CancellationToken cancellationToken = default)
        {
            lock (_refreshLock)
            {
                if (_cachedRefreshTask?.IsCompleted ?? true)
                {
                    _cachedRefreshTask = RefreshTokensCoreAsync(CancellationToken.None);
                }
            }

            // The shared refresh task runs without cancellation so one caller cancelling does not abort
            // the refresh for the others, but each caller can still cancel its own wait on that task.
            return cancellationToken.CanBeCanceled
                ? _cachedRefreshTask.WithCancellationAsync(cancellationToken)
                : _cachedRefreshTask;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> RevokeTokensAsync(CancellationToken cancellationToken = default)
        {
            var formParams = new Dictionary<string, string>
            {
                { "token", _authContext.Tokens.AccessToken },
                { "token_type_hint", "access_token" }
            };
            var encodedCreds = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{_authContext.Credentials.ClientId}:{_authContext.Credentials.ClientSecret}"));

            using (var request = new HttpRequestMessage(HttpMethod.Post, ApiConstants.TokenRevokeEndpoint))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedCreds);
                request.Content = new FormUrlEncodedContent(formParams);

                return await HttpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }


        private async Task<HttpResponseMessage> ExecuteWithTokenRefreshAsync(Func<Task<HttpResponseMessage>> action, bool canRetry = true, CancellationToken cancellationToken = default)
        {
            if (_authContext.DisableAutomaticRefresh || string.IsNullOrEmpty(_authContext.Tokens.RefreshToken))
            {
                return await action().ConfigureAwait(false);
            }
            if (_authContext.Tokens.ExpirationTimeUtc <= DateTime.UtcNow.AddMinutes(1))
            {
                return await RefreshAndExecuteAsync(action, cancellationToken).ConfigureAwait(false);
            }

            var response = await action().ConfigureAwait(false);
            if (canRetry && response.StatusCode == HttpStatusCode.Unauthorized)
            {
                response.Dispose();
                return await RefreshAndExecuteAsync(action, cancellationToken).ConfigureAwait(false);
            }
            return response;
        }

        private async Task<HttpResponseMessage> RefreshAndExecuteAsync(Func<Task<HttpResponseMessage>> action, CancellationToken cancellationToken)
        {
            var refreshResponse = await RefreshTokensAsync(cancellationToken).ConfigureAwait(false);
            if (!refreshResponse.IsSuccessStatusCode)
            {
                return new HttpResponseMessage(refreshResponse.StatusCode);
            }

            return await action().ConfigureAwait(false);
        }


        private async Task<TokenRefreshResponseMessage> RefreshTokensCoreAsync(CancellationToken cancellationToken)
        {
            var formParams = new Dictionary<string, string>
            {
                { "client_id", _authContext.Credentials.ClientId },
                { "client_secret", _authContext.Credentials.ClientSecret },
                { "refresh_token", _authContext.Tokens.RefreshToken },
                { "grant_type", "refresh_token" }
            };
            using (var content = new FormUrlEncodedContent(formParams))
            {
                using (var response = await HttpClient.PostAsync(ApiConstants.TokenRefreshEndpoint, content, cancellationToken).ConfigureAwait(false))
                {
                    var parsedResponse = await TokenRefreshResponseMessage.FromHttpResponseMessageAsync(response, cancellationToken)
                        .ConfigureAwait(false);

                    await HandleTokenRefreshResponseAsync(parsedResponse, cancellationToken)
                        .ConfigureAwait(false);

                    return parsedResponse;
                }
            }
        }

        private async Task<bool> HandleTokenRefreshResponseAsync(TokenRefreshResponseMessage response, CancellationToken cancellationToken)
        {
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var expirationTimeUtc = response.Content.ExpiresIn > 0
                ? DateTime.UtcNow.AddSeconds(response.Content.ExpiresIn)
                : (DateTime?)null;

            AccessToken = response.Content.AccessToken;
            _authContext.Tokens = new TodoistTokens(
                response.Content.AccessToken,
                response.Content.RefreshToken,
                expirationTimeUtc);

            if (_authContext.OnRefresh != null)
            {
                await _authContext.OnRefresh(response.Content, _authContext.RefreshState, cancellationToken)
                    .ConfigureAwait(false);
            }
            return true;
        }
    }
}
