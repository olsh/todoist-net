#if NETSTANDARD2_0

using System;

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
            services.AddSingleton<ITodoistClientFactory, TodoistClientFactory>();
            return services.AddHttpClient(ApiConstants.HttpClientName);
        }

        /// <summary>
        /// Adds todoist client services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configureOptions">An action to configure the <see cref="TodoistClientOptions" />.</param>
        /// <returns>An <see cref="IHttpClientBuilder" /> that can be used to configure the todoist http client.</returns>
        public static IHttpClientBuilder AddTodoistClient(this IServiceCollection services, Action<TodoistClientOptions> configureOptions)
        {
            if (configureOptions != null)
            {
                services.Configure(configureOptions);
            }
            return AddTodoistClient(services);
        }

        /// <summary>
        /// Adds todoist client services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configureOptions">An action to configure the <see cref="TodoistClientOptions" />.</param>
        /// <returns>An <see cref="IHttpClientBuilder" /> that can be used to configure the todoist http client.</returns>
        public static IHttpClientBuilder AddTodoistClient(this IServiceCollection services, Action<IServiceProvider, TodoistClientOptions> configureOptions)
        {
            if (configureOptions != null)
            {
                services
                    .AddOptions<TodoistClientOptions>()
                    .Configure<IServiceProvider>((options, sp) =>
                    {
                        configureOptions(sp, options);
                    });
            }
            return AddTodoistClient(services);
        }
    }
}

#endif
