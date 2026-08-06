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
        private readonly Lazy<IFlurlClient> _shortLivedClient;
        protected Func<IFlurlClient> FlurlClientGetter { get; }

        public TodoistRestClient(string token) : this(token, (IWebProxy)null)
        { }

        public TodoistRestClient(string token, IWebProxy proxy)
        {
            var name = ApiConstants.FlurlClientName + proxy?.GetHashCode();

            // We use long-lived HttpClient instances in cases where IHttpClientFactory is not available (e.g., in .NET Framework).
            // This is to avoid socket exhaustion issues.
            FlurlClientGetter = () => FlurlHttp.Clients.GetOrAdd(name, ApiConstants.ApiBaseUrl, builder => builder
                .ConfigureInnerHandler(handler =>
                {
                    handler.Proxy = proxy;
                    handler.UseProxy = proxy != null;
                })
                .WithOAuthBearerToken(token)
                .AllowAnyHttpStatus()
                .OnError(HandleFlurlError));
        }

        public TodoistRestClient(string token, HttpClient httpClient)
        {
            // We use a short-lived FlurlClient instance here because the HttpClient is provided externally and may have its own lifetime management.
            // This avoids potential issues with reusing a FlurlClient that wraps an externally managed HttpClient.
            _shortLivedClient = new Lazy<IFlurlClient>(() => new FlurlClient(httpClient, ApiConstants.ApiBaseUrl)
                .WithOAuthBearerToken(token)
                .AllowAnyHttpStatus()
                .OnError(HandleFlurlError));

            FlurlClientGetter = () => _shortLivedClient.Value;
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

            var response = await FlurlClientGetter()
                .Request(ApiConstants.ResourcesEndpoint, resource)
                .SetQueryParams(queryParams)
                .GetAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public virtual async Task<HttpResponseMessage> PostAsync(string resource, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            var response = await FlurlClientGetter()
                .Request(ApiConstants.ResourcesEndpoint, resource)
                .PostUrlEncodedAsync(formParams ?? new Dictionary<string, string>(), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public virtual async Task<HttpResponseMessage> PostFilesAsync(string resource, UploadFile[] files, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNull(files, nameof(files));

            var response = await FlurlClientGetter()
                .Request(ApiConstants.ResourcesEndpoint, resource)
                .PostMultipartAsync(mp => mp
                    .AddStringParts(formParams)
                    .AddFileParts("file", files), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public virtual async Task<HttpResponseMessage> PostJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNullOrEmpty(jsonContent, nameof(jsonContent));

            var response = await FlurlClientGetter()
                .Request(ApiConstants.ResourcesEndpoint, resource)
                .PostAsync(new CapturedJsonContent(jsonContent), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public virtual async Task<HttpResponseMessage> PutAsync(string resource, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            var response = await FlurlClientGetter()
                .Request(ApiConstants.ResourcesEndpoint, resource)
                .PutAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public virtual async Task<HttpResponseMessage> PutJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNullOrEmpty(jsonContent, nameof(jsonContent));

            var response = await FlurlClientGetter()
                .Request(ApiConstants.ResourcesEndpoint, resource)
                .PutAsync(new CapturedJsonContent(jsonContent), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public virtual async Task<HttpResponseMessage> DeleteAsync(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            var response = await FlurlClientGetter()
                .Request(ApiConstants.ResourcesEndpoint, resource)
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
    }
}
