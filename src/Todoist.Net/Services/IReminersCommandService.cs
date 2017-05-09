using System;
using System.Net.Http;
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
        /// <returns>
        /// The reminder ID.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reminder" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ComplexId> AddAsync(Reminder reminder);

        /// <summary>
        /// Update the orders of multiple filters at once.
        /// </summary>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task ClearLocationsAsync();

        /// <summary>
        /// Deletes an existing reminder asynchronous.
        /// </summary>
        /// <param name="id">The ID of the reminder.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(ComplexId id);

        /// <summary>
        /// Updates a reminder asynchronous.
        /// </summary>
        /// <param name="reminder">The reminder.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="reminder"/> is <see langword="null"/></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task UpdateAsync(Reminder reminder);
    }
}