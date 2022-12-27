using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Todoist.Net
{
    internal sealed class TodoistRestClient : ITodoistRestClient
    {
        private readonly HttpClient _httpClient;

        public TodoistRestClient() : this(null, null)
        {
        }

        public TodoistRestClient(string token) : this(token, null)
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
                BaseAddress = new Uri("https://api.todoist.com/sync/v9/")
            };

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The response.
        /// </returns>
        /// <exception cref="System.ArgumentException">Value cannot be null or empty - resource</exception>
        /// <exception cref="ArgumentNullException"><paramref name="parameters" /> is <see langword="null" /></exception>
        public async Task<HttpResponseMessage> PostAsync(
            string resource,
            IEnumerable<KeyValuePair<string, string>> parameters)
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
                return await _httpClient.PostAsync(resource, content).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Posts the form asynchronous.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="files">The files.</param>
        /// <returns>The response.</returns>
        public async Task<HttpResponseMessage> PostFormAsync(
            string resource,
            IEnumerable<KeyValuePair<string, string>> parameters,
            IEnumerable<ByteArrayContent> files)
        {
            using (var multipartFormDataContent = new MultipartFormDataContent())
            {
                foreach (var keyValuePair in parameters)
                {
                    multipartFormDataContent.Add(new StringContent(keyValuePair.Value), $"\"{keyValuePair.Key}\"");
                }

                foreach (var file in files)
                {
                    multipartFormDataContent.Add(file, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                }

                return await _httpClient.PostAsync(resource, multipartFormDataContent).ConfigureAwait(false);
            }
        }
    }
}
