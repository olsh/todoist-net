#if NETSTANDARD2_0

using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;

using Todoist.Net.OAuth;

namespace Todoist.Net
{
    internal sealed class TodoistClientFactory : ITodoistClientFactory, ITodoistOAuthClientFactory
    {
        public const string HttpClientName = "Todoist.Net.HttpClient";


        private readonly IHttpMessageHandlerFactory _httpMessageHandlerFactory;

        private readonly IOptionsMonitor<HttpClientFactoryOptions> _httpClientFactoryOptions;
        
        private readonly IOptions<TodoistOAuthOptions> _oAuthOptions;

        public TodoistClientFactory(
            IHttpMessageHandlerFactory httpMessageHandlerFactory,
            IOptionsMonitor<HttpClientFactoryOptions> httpClientFactoryOptions,
            IOptions<TodoistOAuthOptions> oAuthOptions)
        {
            _httpMessageHandlerFactory = httpMessageHandlerFactory;
            _httpClientFactoryOptions = httpClientFactoryOptions;
            _oAuthOptions = oAuthOptions;
        }

        /// <inheritdoc/>
        public TodoistClient CreateClient(string token)
        {
            var innerHandler = _httpMessageHandlerFactory.CreateHandler(HttpClientName);
            var oAuthHandler = new TodoistOAuthHandler(new TodoistTokens(token))
            {
                InnerHandler = innerHandler
            };

            var httpClientActions = _httpClientFactoryOptions
                .Get(HttpClientName)
                .HttpClientActions;

            return new TodoistClient(oAuthHandler, client =>
            {
                foreach (var action in httpClientActions)
                {
                    action(client);
                }
            });
        }

        /// <inheritdoc/>
        public TodoistClient CreateClient(TodoistTokens tokens, Func<TodoistTokens, Task> onTokensRefreshed)
        {
            var options = _oAuthOptions.Value;
            if (string.IsNullOrEmpty(options.ClientId))
            {
                throw new InvalidOperationException(
                    "The OAuth client ID is not configured. Pass it to the AddTodoistClient overload which configures TodoistOAuthOptions.");
            }

            // The OAuth handler holds the tokens of a single user, so it wraps the pooled handlers instead of joining them.
            // That means creating the HttpClient here, so it gets the configuration IHttpClientFactory applies to the clients it creates.
            var innerHandler = _httpMessageHandlerFactory.CreateHandler(HttpClientName);
            var oAuthHandler = new TodoistOAuthHandler(tokens, options, onTokensRefreshed)
            {
                InnerHandler = innerHandler
            };

            var httpClientActions = _httpClientFactoryOptions
                .Get(HttpClientName)
                .HttpClientActions;

            return new TodoistClient(oAuthHandler, client =>
            {
                foreach (var action in httpClientActions)
                {
                    action(client);
                }
            });
        }
    }
}

#endif
