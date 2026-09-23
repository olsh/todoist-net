#if NETSTANDARD2_0

using System;
using System.Net.Http;

using Microsoft.Extensions.Options;

namespace Todoist.Net
{
    internal sealed class TodoistClientFactory : ITodoistClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<TodoistClientOptions> _options;

        public TodoistClientFactory(IHttpClientFactory httpClientFactory, IOptions<TodoistClientOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options;
        }

        /// <inheritdoc/>
        public TodoistClient CreateClient(string legacyToken)
        {
            var httpClient = _httpClientFactory.CreateClient(ApiConstants.HttpClientName);

            var todoistRestClient = new TodoistRestClient(legacyToken, httpClient);
            return new TodoistClient(todoistRestClient);
        }

        /// <inheritdoc/>
        public TodoistClient CreateClient(TodoistTokens tokens)
        {
            var options = _options.Value;
            if (string.IsNullOrEmpty(options?.Credentials?.ClientId) || string.IsNullOrEmpty(options?.Credentials?.ClientSecret))
            {
                throw new InvalidOperationException("TodoistClientOptions must be configured properly in DI to use the CreateClient(TodoistTokens) method.");
            }

            var authContext = new TodoistAuthenticationContext(options.Credentials, tokens, options.OnRefresh, options.DisableAutomaticRefresh);
            var httpClient = _httpClientFactory.CreateClient(ApiConstants.HttpClientName);

            var todoistRestClient = new RefreshableTodoistRestClient(authContext, httpClient);
            return new TodoistClient(todoistRestClient);
        }

        /// <inheritdoc/>
        public TodoistClient CreateClient(TodoistTokens tokens, object refreshState)
        {
            var options = _options.Value;
            if (string.IsNullOrEmpty(options?.Credentials?.ClientId) || string.IsNullOrEmpty(options?.Credentials?.ClientSecret))
            {
                throw new InvalidOperationException("TodoistClientOptions must be configured properly in DI to use the CreateClient(TodoistTokens) method.");
            }

            var authContext = new TodoistAuthenticationContext(options.Credentials, tokens, options.OnRefresh, refreshState, options.DisableAutomaticRefresh);
            var httpClient = _httpClientFactory.CreateClient(ApiConstants.HttpClientName);

            var todoistRestClient = new RefreshableTodoistRestClient(authContext, httpClient);
            return new TodoistClient(todoistRestClient);
        }
    }
}

#endif
