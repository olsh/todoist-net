#if NETSTANDARD2_0

using System.Net.Http;

using Todoist.Net.Extensions;

namespace Todoist.Net
{
    internal sealed class TodoistClientFactory : ITodoistClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TodoistClientFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <inheritdoc/>
        public TodoistClient CreateClient(string token)
        {
            var httpClient = _httpClientFactory.CreateClient(ApiConstants.HttpClientName);
            var todoistRestClient = new TodoistRestClient(token, httpClient);

            return new TodoistClient(todoistRestClient);
        }
    }
}

#endif
