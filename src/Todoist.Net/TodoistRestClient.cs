using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Flurl.Http.Content;

using Todoist.Net.Exceptions;
using Todoist.Net.Extensions;
using Todoist.Net.Models;

namespace Todoist.Net
{
    internal sealed class TodoistRestClient : ITodoistRestClient
    {
        private readonly IFlurlClient _flurlClient;

        public TodoistRestClient(string token) : this(token, (IWebProxy)null)
        { }

        public TodoistRestClient(string token, IWebProxy proxy)
        {
            _flurlClient = new FlurlClientBuilder(ApiConstants.ApiBaseUrl)
                .ConfigureInnerHandler(handler =>
                {
                    handler.Proxy = proxy;
                    handler.UseProxy = proxy != null;
                })
                .WithOAuthBearerToken(token)
                .AllowAnyHttpStatus()
                .OnError(HandleFlurlError)
                .Build();
        }

        public TodoistRestClient(string token, HttpClient httpClient)
        {
            _flurlClient = new FlurlClient(httpClient, ApiConstants.ApiBaseUrl)
                .WithOAuthBearerToken(token)
                .AllowAnyHttpStatus()
                .OnError(HandleFlurlError);
        }

        void IDisposable.Dispose() { }


        /// <inheritdoc/>
        public async Task<HttpResponseMessage> GetAsync(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            var response = await _flurlClient
                .Request(ApiConstants.ResourcesEndpoint, resource)
                .SetQueryParams(queryParams)
                .GetAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostAsync(string resource, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            var response = await _flurlClient
                .Request(ApiConstants.ResourcesEndpoint, resource)
                .PostUrlEncodedAsync(formParams ?? new Dictionary<string, string>(), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostFilesAsync(string resource, UploadFile[] files, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNull(files, nameof(files));

            var response = await _flurlClient
                .Request(ApiConstants.ResourcesEndpoint, resource)
                .PostMultipartAsync(mp => mp
                    .AddStringParts(formParams)
                    .AddFileParts("file", files), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PostJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNullOrEmpty(jsonContent, nameof(jsonContent));

            var response = await _flurlClient
                .Request(ApiConstants.ResourcesEndpoint, resource)
                .PostAsync(new CapturedJsonContent(jsonContent), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PutAsync(string resource, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            var response = await _flurlClient
                .Request(ApiConstants.ResourcesEndpoint, resource)
                .PutAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> PutJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));
            ThrowHelper.ThrowIfNullOrEmpty(jsonContent, nameof(jsonContent));

            var response = await _flurlClient
                .Request(ApiConstants.ResourcesEndpoint, resource)
                .PutAsync(new CapturedJsonContent(jsonContent), cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> DeleteAsync(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(resource, nameof(resource));

            var response = await _flurlClient
                .Request(ApiConstants.ResourcesEndpoint, resource)
                .SetQueryParams(queryParams)
                .DeleteAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response.ResponseMessage;
        }


        private static void HandleFlurlError(FlurlCall call)
        {
            if (call.Exception is HttpRequestException)
            {
                // Users of this library expect to receive HttpRequestException when the request fails, not FlurlHttpException.
                throw call.Exception;
            }
        }
    }
}
