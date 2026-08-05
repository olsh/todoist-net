#if NETSTANDARD2_0

using Microsoft.Extensions.DependencyInjection;

namespace Todoist.Net.Extensions
{
    /// <summary>
    /// Extension methods for setting up todoist client services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class TodoistServiceCollectionExtensions
    {
        /// <summary>
        /// Adds todoist client services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>An <see cref="IHttpClientBuilder" /> that can be used to configure the todoist http client.</returns>
        public static IHttpClientBuilder AddTodoistClient(this IServiceCollection services)
        {
            var builder = services.AddHttpClient(ApiConstants.HttpClientName);
            services.AddSingleton<ITodoistClientFactory, TodoistClientFactory>();

            return builder;
        }
    }
}

#endif
