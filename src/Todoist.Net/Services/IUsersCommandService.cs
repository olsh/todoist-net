using System;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for users management which can be executed in a transaction.
    /// </summary>
    public interface IUsersCommandService
    {
        /// <summary>
        /// Gets the current user info.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The current user info.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="user" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task UpdateAsync(User user);

        /// <summary>
        /// Gets the current user info.
        /// </summary>
        /// <param name="karmaGoals">The karma goals.</param>
        /// <returns>The current user info.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="karmaGoals" /> is <see langword="null" /></exception>
        Task UpdateKarmaGoalsAsync(KarmaGoals karmaGoals);
    }
}
