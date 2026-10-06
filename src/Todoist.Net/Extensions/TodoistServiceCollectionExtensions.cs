#if NETSTANDARD2_0

using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Todoist.Net.Exceptions;
using Todoist.Net.OAuth;

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
            services.TryAddSingleton<TodoistClientFactory>();
            services.TryAddSingleton<ITodoistClientFactory>(provider =>
                provider.GetRequiredService<TodoistClientFactory>());
            services.TryAddSingleton<ITodoistOAuthClientFactory>(provider =>
                provider.GetRequiredService<TodoistClientFactory>());

            return services;
        }

        /// <summary>
        /// Adds todoist client services to the specified <see cref="IServiceCollection" />, including
        /// <see cref="ITodoistOAuthClientFactory" /> which creates clients authorized with the OAuth tokens of users.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configureOAuth">Configures the credentials of the application the OAuth tokens were issued to.</param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection AddTodoistClient(
            this IServiceCollection services,
            Action<TodoistOAuthOptions> configureOAuth)
        {
            ThrowHelper.ThrowIfNull(configureOAuth, nameof(configureOAuth));

            services.Configure(configureOAuth);

            return services.AddTodoistClient();
        }
    }
}

#endif
