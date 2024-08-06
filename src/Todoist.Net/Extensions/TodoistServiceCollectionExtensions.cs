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
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection AddTodoistClient(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<ITodoistClientFactory, TodoistClientFactory>();

            return services;
        }
    }
}

#endif
