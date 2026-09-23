using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Extensions;
using Todoist.Net.Models;

namespace Todoist.Net
{
    internal class TodoistRestClient : ITodoistRestClient
    {
        private static readonly Lazy<HttpClient> _defaultClient = new Lazy<HttpClient>(() => CreateClient());
        private static readonly ConcurrentDictionary<IWebProxy, HttpClient> _proxiedClients = new ConcurrentDictionary<IWebProxy, HttpClient>();

        protected string AccessToken { get; set; }
        protected HttpClient HttpClient { get; }

        public TodoistRestClient(string token) : this(token, (IWebProxy)null)
        { }

        public TodoistRestClient(string token, IWebProxy proxy)
        {
            ThrowHelper.ThrowIfNullOrEmpty(token, nameof(token));

            // We use long-lived HttpClient instances in cases where IHttpClientFactory is not available (e.g., in .NET Framework).
            // This is to avoid socket exhaustion issues.
            AccessToken = token;
            HttpClient = proxy == null
                ? _defaultClient.Value
                : _proxiedClients.GetOrAdd(proxy, CreateClient);
        }

        public TodoistRestClient(string token, HttpClient httpClient)
        {
            ThrowHelper.ThrowIfNullOrEmpty(token, nameof(token));
            ThrowHelper.ThrowIfNull(httpClient, nameof(httpClient));

            // We use the provided short-lived HttpClient instance here because it has its own lifetime management.
            AccessToken = token;
            HttpClient = httpClient;

            HttpClient.BaseAddress = new Uri(ApiConstants.ApiBaseUrl);
        }


        protected virtual void Dispose(bool disposing) { }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <inheritdoc/>
        public async virtual Task<HttpResponseMessage> GetAsync(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            using (var request = BuildResourceRequest(HttpMethod.Get, resource, queryParams))
            {
                return await HttpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async virtual Task<HttpResponseMessage> PostAsync(string resource, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            using (var request = BuildResourceRequest(HttpMethod.Post, resource))
            {
                request.Content = new FormUrlEncodedContent(formParams ?? new Dictionary<string, string>());

                return await HttpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async virtual Task<HttpResponseMessage> PostFilesAsync(string resource, UploadFile[] files, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNull(files, nameof(files));

            using (var request = BuildResourceRequest(HttpMethod.Post, resource))
            {
                request.Content = new MultipartFormDataContent()
                    .AddStringParts(formParams)
                    .AddFileParts("file", files);

                return await HttpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async virtual Task<HttpResponseMessage> PostJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNullOrEmpty(jsonContent, nameof(jsonContent));

            using (var request = BuildResourceRequest(HttpMethod.Post, resource))
            {
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                return await HttpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async virtual Task<HttpResponseMessage> PutAsync(string resource, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            using (var request = BuildResourceRequest(HttpMethod.Put, resource))
            {
                return await HttpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async virtual Task<HttpResponseMessage> PutJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNullOrEmpty(jsonContent, nameof(jsonContent));

            using (var request = BuildResourceRequest(HttpMethod.Put, resource))
            {
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                return await HttpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async virtual Task<HttpResponseMessage> DeleteAsync(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            using (var request = BuildResourceRequest(HttpMethod.Delete, resource, queryParams))
            {
                return await HttpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }


        private HttpRequestMessage BuildResourceRequest(HttpMethod method, string resource, Dictionary<string, string> queryParams = null)
        {
            var requestUri = $"{ApiConstants.ResourcesEndpoint}/{resource}{BuildQuerySegment(queryParams)}";

            var request = new HttpRequestMessage(method, requestUri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            return request;
        }

        private static string BuildQuerySegment(Dictionary<string, string> queryParams)
        {
            string encode(string data) => string.IsNullOrEmpty(data)
                ? string.Empty
                : Uri.EscapeDataString(data).Replace("%20", "+");

            if (queryParams == null || queryParams.Count == 0)
            {
                return string.Empty;
            }
            var builder = new StringBuilder();

            foreach (var pair in queryParams)
            {
                if (builder.Length > 0)
                {
                    builder.Append('&');
                }
                builder.Append(encode(pair.Key));
                builder.Append('=');
                builder.Append(encode(pair.Value));
            }
            return "?" + builder.ToString();
        }

        private static HttpClient CreateClient(IWebProxy proxy = null)
        {
            var handler = new HttpClientHandler();
            if (proxy != null)
            {
                handler.Proxy = proxy;
                handler.UseProxy = true;
            }
            return new HttpClient(handler)
            {
                BaseAddress = new Uri(ApiConstants.ApiBaseUrl)
            };
        }
    }
}
