using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;
using Todoist.Net.OAuth;

namespace Todoist.Net
{
    internal sealed class TodoistRestClient : ITodoistRestClient
    {
        private readonly HttpClient _httpClient;

        public TodoistRestClient(TodoistOAuthHandler todoistOAuthHandler, Action<HttpClient> configureHttpClient = null)
        {
            ThrowHelper.ThrowIfNull(todoistOAuthHandler, nameof(todoistOAuthHandler));

            _httpClient = new HttpClient(todoistOAuthHandler);
            configureHttpClient?.Invoke(_httpClient);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }


        /// <inheritdoc/>
        public async Task<HttpResponseMessage> GetAsync(
            string resource,
            Dictionary<string, string> queryParams = null,
            CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            using (var request = ApiRequestBuilder.BuildResourceRequest(HttpMethod.Get, resource, queryParams))
            {
                return await _httpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostAsync(
            string resource,
            Dictionary<string, string> formParams = null,
            CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            formParams = formParams ?? new Dictionary<string, string>();

            using (var request = ApiRequestBuilder.BuildResourceFormRequest(resource, formParams))
            {
                return await _httpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostFilesAsync(
            string resource,
            UploadFile[] files,
            Dictionary<string, string> formParams = null,
            CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNull(files, nameof(files));

            formParams = formParams ?? new Dictionary<string, string>();
            
            using (var request = ApiRequestBuilder.BuildResourceFormRequest(resource, formParams, files))
            {
                return await _httpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostJsonAsync(
            string resource,
            string jsonContent,
            CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNullOrEmpty(jsonContent, nameof(jsonContent));

            using (var request = ApiRequestBuilder.BuildResourceJsonRequest(HttpMethod.Post, resource, jsonContent))
            {
                return await _httpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PutAsync(string resource, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            using (var request = ApiRequestBuilder.BuildResourceRequest(HttpMethod.Put, resource))
            {
                return await _httpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PutJsonAsync(
            string resource,
            string jsonContent,
            CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNullOrEmpty(jsonContent, nameof(jsonContent));

            using (var request = ApiRequestBuilder.BuildResourceJsonRequest(HttpMethod.Put, resource, jsonContent))
            {
                return await _httpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> DeleteAsync(
            string resource,
            Dictionary<string, string> queryParams = null,
            CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            using (var request = ApiRequestBuilder.BuildResourceRequest(HttpMethod.Delete, resource, queryParams))
            {
                return await _httpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }
    }
}
