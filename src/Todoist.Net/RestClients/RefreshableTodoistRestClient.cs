using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Flurl.Http;

using Todoist.Net.Models;

namespace Todoist.Net
{
    internal class RefreshableTodoistRestClient : TodoistRestClient, IRefreshableTodoistRestClient
    {
        private readonly TodoistAuthenticationContext _authContext;

        public RefreshableTodoistRestClient(TodoistAuthenticationContext authContext) : base(authContext.Tokens.AccessToken)
        {
            _authContext = authContext;
        }

        public RefreshableTodoistRestClient(TodoistAuthenticationContext authContext, IWebProxy proxy) : base(authContext.Tokens.AccessToken, proxy)
        {
            _authContext = authContext;
        }

        public RefreshableTodoistRestClient(TodoistAuthenticationContext authContext, HttpClient httpClient) : base(authContext.Tokens.AccessToken, httpClient)
        {
            _authContext = authContext;
        }


        /// <inheritdoc/>
        public override Task<HttpResponseMessage> GetAsync(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default)
        {
            return ExecuteWithTokenRefreshAsync(() =>
                base.GetAsync(resource, queryParams, cancellationToken), cancellationToken);
        }

        /// <inheritdoc/>
        public override Task<HttpResponseMessage> PostAsync(string resource, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default)
        {
            return ExecuteWithTokenRefreshAsync(() =>
                base.PostAsync(resource, formParams, cancellationToken), cancellationToken);
        }

        /// <inheritdoc/>
        public override Task<HttpResponseMessage> PostFilesAsync(string resource, UploadFile[] files, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default)
        {
            return ExecuteWithTokenRefreshAsync(() =>
                base.PostFilesAsync(resource, files, formParams, cancellationToken), cancellationToken);
        }

        /// <inheritdoc/>
        public override Task<HttpResponseMessage> PostJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
        {
            return ExecuteWithTokenRefreshAsync(() =>
                base.PostJsonAsync(resource, jsonContent, cancellationToken), cancellationToken);
        }

        /// <inheritdoc/>
        public override Task<HttpResponseMessage> PutAsync(string resource, CancellationToken cancellationToken = default)
        {
            return ExecuteWithTokenRefreshAsync(() =>
                base.PutAsync(resource, cancellationToken), cancellationToken);
        }

        /// <inheritdoc/>
        public override Task<HttpResponseMessage> PutJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
        {
            return ExecuteWithTokenRefreshAsync(() =>
                base.PutJsonAsync(resource, jsonContent, cancellationToken), cancellationToken);
        }

        /// <inheritdoc/>
        public override Task<HttpResponseMessage> DeleteAsync(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default)
        {
            return ExecuteWithTokenRefreshAsync(() => 
                base.DeleteAsync(resource, queryParams, cancellationToken), cancellationToken);
        }


        /// <inheritdoc/>
        public async Task<HttpResponseMessage> RefreshTokensAsync(CancellationToken cancellationToken = default)
        {
            var response = await FlurlClient
                .Request(ApiConstants.TokenRefreshEndpoint)
                .PostUrlEncodedAsync(new
                {
                    client_id = _authContext.Credentials.ClientId,
                    client_secret = _authContext.Credentials.ClientSecret,
                    refresh_token = _authContext.Tokens.RefreshToken,
                    grant_type = "refresh_token"
                }, cancellationToken: cancellationToken);

            if (!response.ResponseMessage.IsSuccessStatusCode)
            {
                return response.ResponseMessage;
            }
            var jsonResponse = await GetJsonAndResetBufferAsync(response.ResponseMessage).ConfigureAwait(false);

            AccessToken = jsonResponse.AccessToken;
            _authContext.Tokens = new TodoistTokens(
                jsonResponse.AccessToken,
                jsonResponse.RefreshToken,
                DateTime.UtcNow.AddSeconds(jsonResponse.ExpiresIn));

            if (_authContext.OnRefresh != null)
            {
                await _authContext.OnRefresh(jsonResponse, cancellationToken).ConfigureAwait(false);
            }
            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> RevokeTokensAsync(CancellationToken cancellationToken = default)
        {
            var response = await FlurlClient
                .Request(ApiConstants.TokenRevokeEndpoint)
                .WithBasicAuth(_authContext.Credentials.ClientId, _authContext.Credentials.ClientSecret)
                .PostUrlEncodedAsync(new
                {
                    token = _authContext.Tokens.AccessToken,
                    token_type_hint = "access_token"
                }, cancellationToken: cancellationToken);

            return response.ResponseMessage;
        }


        private async Task<HttpResponseMessage> ExecuteWithTokenRefreshAsync(Func<Task<HttpResponseMessage>> action, CancellationToken cancellationToken)
        {
            bool tokenFoundExpired = _authContext.Tokens.ExpirationTimeUtc <= DateTime.UtcNow.AddMinutes(1);
            if (tokenFoundExpired)
            {
                await RefreshTokensAsync(cancellationToken).ConfigureAwait(false);
            }

            var response = await action().ConfigureAwait(false);
            if (!tokenFoundExpired && response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshResponse = await RefreshTokensAsync(cancellationToken).ConfigureAwait(false);
                if (refreshResponse.IsSuccessStatusCode)
                {
                    response = await action().ConfigureAwait(false);
                }
            }
            return response;
        }

        private static async Task<TokenRefreshResponse> GetJsonAndResetBufferAsync(HttpResponseMessage response)
        {
            using (var originalContent = response.Content)
            {
                var responseBody = await originalContent.ReadAsByteArrayAsync().ConfigureAwait(false);

                var bufferedContent = new ByteArrayContent(responseBody);
                foreach (var header in originalContent.Headers)
                {
                    bufferedContent.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }

                response.Content = bufferedContent;
                return JsonSerializer.Deserialize<TokenRefreshResponse>(responseBody);
            }
        }
    }
}
