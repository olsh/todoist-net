using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net
{
    internal sealed class TodoistRestClient : ITodoistRestClient
    {
        private const string ApiBaseUrl = "https://api.todoist.com/api/v1/";

        private readonly HttpClient _httpClient;
        private readonly bool _disposeHttpClient;

        public TodoistRestClient() : this(null, (IWebProxy)null)
        { }

        public TodoistRestClient(string token) : this(token, (IWebProxy)null)
        { }

        public TodoistRestClient(IWebProxy proxy) : this(null, proxy)
        { }

        public TodoistRestClient(string token, IWebProxy proxy)
        {
            var httpClientHandler = new HttpClientHandler();
            if (proxy != null)
            {
                httpClientHandler.Proxy = proxy;
                httpClientHandler.UseProxy = true;
            }

            // ReSharper disable once ExceptionNotDocumented
            _httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri(ApiBaseUrl)
            };

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            _disposeHttpClient = true;
        }

        public TodoistRestClient(string token, HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(ApiBaseUrl);
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public void Dispose()
        {
            if (_disposeHttpClient)
            {
                _httpClient?.Dispose();
            }
        }


        /// <inheritdoc/>
        public async Task<HttpResponseMessage> GetAsync(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            var requestUri = await AppendQueryParamsAsync(resource, queryParams).ConfigureAwait(false);

            return await _httpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostAsync(string resource, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            
            formParams = formParams ?? new Dictionary<string, string>();
            using (var content = new FormUrlEncodedContent(formParams))
            {
                return await _httpClient.PostAsync(resource, content, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostFilesAsync(string resource, UploadFile[] files, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNull(files, nameof(files));

            formParams = formParams ?? new Dictionary<string, string>();
            using (var multipartFormDataContent = new MultipartFormDataContent())
            {
                BuildFormDataContent(multipartFormDataContent, formParams, files);

                return await _httpClient.PostAsync(resource, multipartFormDataContent, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNullOrEmpty(jsonContent, nameof(jsonContent));

            using (var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json"))
            {
                return await _httpClient.PostAsync(resource, content, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public Task<HttpResponseMessage> PutAsync(string resource, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            
            var content = new StringContent(string.Empty);

            return _httpClient.PutAsync(resource, content, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PutJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNullOrEmpty(jsonContent, nameof(jsonContent));

            using (var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json"))
            {
                return await _httpClient.PutAsync(resource, content, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> DeleteAsync(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            var requestUri = await AppendQueryParamsAsync(resource, queryParams).ConfigureAwait(false);

            return await _httpClient.DeleteAsync(requestUri, cancellationToken).ConfigureAwait(false);
        }


        private static async Task<string> AppendQueryParamsAsync(string resource, Dictionary<string, string> queryParams)
        {
            if (queryParams == null || queryParams.Count == 0)
            {
                return resource;
            }

            using (var content = new FormUrlEncodedContent(queryParams))
            {
                var query = await content.ReadAsStringAsync().ConfigureAwait(false);
                return $"{resource}?{query}";
            }
        }

        private static void BuildFormDataContent(MultipartFormDataContent multipartFormDataContent, Dictionary<string, string> formParams, UploadFile[] files)
        {
            foreach (var keyValuePair in formParams)
            {
                multipartFormDataContent.Add(new StringContent(keyValuePair.Value), $"\"{keyValuePair.Key}\"");
            }

            foreach (var file in files)
            {
                var content = new StreamContent(file.ContentStream);
                if (file.MimeType != null && MediaTypeHeaderValue.TryParse(file.MimeType, out var mediaType))
                {
                    content.Headers.ContentType = mediaType;
                }

                multipartFormDataContent.Add(content, "file", file.Filename);
            }
        }
    }
}
