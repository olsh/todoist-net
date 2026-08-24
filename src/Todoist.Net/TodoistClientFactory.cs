#if NETSTANDARD2_0

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

namespace Todoist.Net
{
    internal sealed class TodoistClientFactory : ITodoistClientFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<TodoistClientOptions> _options;

        public TodoistClientFactory(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory, IOptions<TodoistClientOptions> options)
        {
            _serviceProvider = serviceProvider;
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
        public TodoistClient CreateClient(TodoistTokens tokens, object refreshState = null)
        {
            if (string.IsNullOrEmpty(_options.Value?.Credentials?.ClientId) || string.IsNullOrEmpty(_options.Value?.Credentials?.ClientSecret))
            {
                throw new InvalidOperationException("TodoistClientOptions must be configured properly in DI to use the CreateClient(TodoistTokens) method.");
            }

            Task refreshCallback(TokenRefreshResponse res, object state, CancellationToken ct)
            {
                if (_options.Value.OnRefresh == null)
                {
                    return Task.CompletedTask;
                }
                return _options.Value.OnRefresh(_serviceProvider, res, state, ct);
            }

            var authContext = new TodoistAuthenticationContext(_options.Value.Credentials, tokens, refreshCallback, refreshState);
            var httpClient = _httpClientFactory.CreateClient(ApiConstants.HttpClientName);

            var todoistRestClient = new RefreshableTodoistRestClient(authContext, httpClient);
            return new TodoistClient(todoistRestClient);
        }
    }
}

#endif
