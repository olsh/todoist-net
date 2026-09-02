using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for user management which can be executed in a transaction.
    /// </summary>
    public interface IUserCommandService
    {
        /// <summary>
        /// Updates the current user info.
        /// </summary>
        /// <param name="user">The user update payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="user" /> is <see langword="null" /></exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task UpdateAsync(UpdateUser user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the current user karma goals.
        /// </summary>
        /// <param name="karmaGoals">The karma goals update payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="karmaGoals" /> is <see langword="null" /></exception>
        Task UpdateKarmaGoalsAsync(UpdateKarmaGoals karmaGoals, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates user settings.
        /// </summary>
        /// <param name="settings">The user settings update payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="settings" /> is <see langword="null" />.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task UpdateSettingsAsync(UpdateUserSettings settings, CancellationToken cancellationToken = default);
    }
}
