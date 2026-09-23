#if NETSTANDARD2_0

using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using Todoist.Net.OAuth;

namespace Todoist.Net
{
    internal sealed class TodoistClientFactory : ITodoistClientFactory, ITodoistOAuthClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IHttpMessageHandlerFactory _httpMessageHandlerFactory;

        private readonly IOptions<TodoistOAuthOptions> _oauthOptions;

        public TodoistClientFactory(
            IHttpClientFactory httpClientFactory,
            IHttpMessageHandlerFactory httpMessageHandlerFactory,
            IOptions<TodoistOAuthOptions> oauthOptions)
        {
            _httpClientFactory = httpClientFactory;
            _httpMessageHandlerFactory = httpMessageHandlerFactory;
            _oauthOptions = oauthOptions;
        }

        /// <inheritdoc/>
        public TodoistClient CreateClient(string token)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var todoistRestClient = new TodoistRestClient(token, httpClient);

            return new TodoistClient(todoistRestClient);
        }

        /// <inheritdoc/>
        public TodoistClient CreateClient(TodoistTokens tokens, Func<TodoistTokens, Task> onTokensRefreshed)
        {
            var options = _oauthOptions.Value;
            if (string.IsNullOrEmpty(options.ClientId))
            {
                throw new InvalidOperationException(
                    "The OAuth client ID is not configured. Pass it to the AddTodoistClient overload which configures TodoistOAuthOptions.");
            }

            // The OAuth handler holds the tokens of a single user, so it wraps the pooled handlers instead of joining them.
            var innerHandler = _httpMessageHandlerFactory.CreateHandler();

            return TodoistClient.CreateOAuthClient(options, tokens, onTokensRefreshed, innerHandler);
        }
    }
}

#endif
