using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Flurl;
using Flurl.Http;
using Flurl.Http.Content;

using Todoist.Net.Exceptions;
using Todoist.Net.Extensions;
using Todoist.Net.Models;

namespace Todoist.Net
{
    internal class TodoistRestClient : ITodoistRestClient
    {
        protected string AccessToken { get; set; }
        protected IFlurlClient FlurlClient { get; }

        public TodoistRestClient(string token) : this(token, (IWebProxy)null)
        { }

        public TodoistRestClient(string token, IWebProxy proxy)
        {
            AccessToken = token;

            // We use long-lived HttpClient instances in cases where IHttpClientFactory is not available (e.g., in .NET Framework).
            // This is to avoid socket exhaustion issues.
            FlurlClient = FlurlHttp.Clients.GetOrAdd(
                name: ApiConstants.FlurlClientName + proxy?.GetHashCode(),
                baseUrl: ApiConstants.ApiBaseUrl,
                configure: builder => builder
                    .ConfigureInnerHandler(handler =>
                    {
                        handler.Proxy = proxy;
                        handler.UseProxy = proxy != null;
                    })
                    .AllowAnyHttpStatus()
                    .OnError(HandleFlurlError));
        }

        public TodoistRestClient(string token, HttpClient httpClient)
        {
            AccessToken = token;

            // We use a short-lived FlurlClient instance here because the HttpClient is provided externally and may have its own lifetime management.
            // This avoids potential issues with reusing a FlurlClient that wraps an externally managed HttpClient.
            FlurlClient = new FlurlClient(httpClient, ApiConstants.ApiBaseUrl)
                .AllowAnyHttpStatus()
                .OnError(HandleFlurlError);
        }


        protected virtual void Dispose(bool disposing) { }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <inheritdoc/>
        public virtual async Task<HttpResponseMessage> GetAsync(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            var response = await BuildResourceRequest(resource)
                .SetQueryParams(queryParams)
                .GetAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public virtual async Task<HttpResponseMessage> PostAsync(string resource, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            var response = await BuildResourceRequest(resource)
                .PostUrlEncodedAsync(formParams ?? new Dictionary<string, string>(), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public virtual async Task<HttpResponseMessage> PostFilesAsync(string resource, UploadFile[] files, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNull(files, nameof(files));

            var response = await BuildResourceRequest(resource)
                .PostMultipartAsync(mp => mp
                    .AddStringParts(formParams ?? new Dictionary<string, string>())
                    .AddFileParts("file", files), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public virtual async Task<HttpResponseMessage> PostJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNullOrEmpty(jsonContent, nameof(jsonContent));

            var response = await BuildResourceRequest(resource)
                .PostAsync(new CapturedJsonContent(jsonContent), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public virtual async Task<HttpResponseMessage> PutAsync(string resource, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            var response = await BuildResourceRequest(resource)
                .PutAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public virtual async Task<HttpResponseMessage> PutJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNullOrEmpty(jsonContent, nameof(jsonContent));

            var response = await BuildResourceRequest(resource)
                .PutAsync(new CapturedJsonContent(jsonContent), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public virtual async Task<HttpResponseMessage> DeleteAsync(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            var response = await BuildResourceRequest(resource)
                .SetQueryParams(queryParams)
                .DeleteAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }


        protected virtual void HandleFlurlError(FlurlCall call)
        {
            if (call.Exception is HttpRequestException)
            {
                // Users of this library expect to receive HttpRequestException when the request fails, not FlurlHttpException.
                throw call.Exception;
            }
        }

        private IFlurlRequest BuildResourceRequest(string resource)
        {
            return FlurlClient
                .Request(ApiConstants.ResourcesEndpoint, resource)
                .WithOAuthBearerToken(AccessToken);
        }
    }
}
