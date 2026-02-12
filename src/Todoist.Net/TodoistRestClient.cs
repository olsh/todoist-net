using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net
{
    internal sealed class TodoistRestClient : ITodoistRestClient
    {
        private readonly HttpClient _httpClient;
        private readonly bool _disposeHttpClient;

        public TodoistRestClient() : this(null, (IWebProxy)null)
        {
        }

        public TodoistRestClient(string token) : this(token, (IWebProxy)null)
        {
        }

        public TodoistRestClient(IWebProxy proxy) : this(null, proxy)
        {
        }

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
                BaseAddress = new Uri("https://api.todoist.com/api/v1/")
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

            _httpClient.BaseAddress = new Uri("https://api.todoist.com/api/v1/");
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
        public async Task<HttpResponseMessage> GetAsync(
            string resource,
            IEnumerable<KeyValuePair<string, string>> parameters,
            CancellationToken cancellationToken = default)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (string.IsNullOrEmpty(resource))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(resource));
            }

            string requestUri;
            using (var content = new FormUrlEncodedContent(parameters))
            {
                var query = await content.ReadAsStringAsync().ConfigureAwait(false);
                requestUri = $"{resource}?{query}";
            }
            return await _httpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostAsync(
            string resource,
            IEnumerable<KeyValuePair<string, string>> parameters,
            CancellationToken cancellationToken = default)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (string.IsNullOrEmpty(resource))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(resource));
            }

            using (var content = new FormUrlEncodedContent(parameters))
            {
                return await _httpClient.PostAsync(resource, content, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostFormAsync(
            string resource,
            IEnumerable<KeyValuePair<string, string>> parameters,
            IEnumerable<UploadFile> files,
            CancellationToken cancellationToken = default)
        {
            using (var multipartFormDataContent = new MultipartFormDataContent())
            {
                foreach (var keyValuePair in parameters)
                {
                    multipartFormDataContent.Add(new StringContent(keyValuePair.Value), $"\"{keyValuePair.Key}\"");
                }

                foreach (var file in files)
                {
                    var content = new ByteArrayContent(file.Content);
                    if (file.MimeType != null && MediaTypeHeaderValue.TryParse(file.MimeType, out var mediaType))
                    {
                        content.Headers.ContentType = mediaType;
                    }

                    multipartFormDataContent.Add(content, "file", file.Filename);
                }

                return await _httpClient.PostAsync(resource, multipartFormDataContent, cancellationToken)
                                        .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostJsonAsync(
            string resource,
            string jsonContent,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(resource))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(resource));
            }

            using (var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json"))
            {
                return await _httpClient.PostAsync(resource, content, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PutAsync(
            string resource,
            string jsonContent,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(resource))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(resource));
            }

            using (var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json"))
            {
                return await _httpClient.PutAsync(resource, content, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> DeleteAsync(
            string resource,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(resource))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(resource));
            }

            return await _httpClient.DeleteAsync(resource, cancellationToken).ConfigureAwait(false);
        }
    }
}
