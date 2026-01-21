using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for reminders management.
    /// </summary>
    public interface IRemindersCommandService
    {
        /// <summary>
        /// Adds a reminder asynchronous.
        /// </summary>
        /// <param name="reminder">The reminder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The reminder ID.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reminder" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ComplexId> AddAsync(Reminder reminder, CancellationToken cancellationToken = default);

        /// <summary>
        /// Clears the locations of all location-based reminders.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task ClearLocationsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an existing reminder asynchronous.
        /// </summary>
        /// <param name="id">The ID of the reminder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a reminder asynchronous.
        /// </summary>
        /// <param name="reminder">The reminder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="reminder"/> is <see langword="null"/></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task UpdateAsync(Reminder reminder, CancellationToken cancellationToken = default);
    }
}
