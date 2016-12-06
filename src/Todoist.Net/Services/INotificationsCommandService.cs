using System;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist notification management which can be executes in a transaction.
    /// </summary>
    public interface INotificationsCommandService
    {
        /// <summary>
        /// Marks the last read live notification.
        /// </summary>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task MarkAllReadAsync();

        /// <summary>
        /// Marks the last read live notification.
        /// </summary>
        /// <param name="id">The ID of the last read notification.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task MarkLastReadAsync(ComplexId id);
    }
}