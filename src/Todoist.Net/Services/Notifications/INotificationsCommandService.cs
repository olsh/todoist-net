using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist notification management which can be executes in a transaction.
    /// </summary>
    public interface INotificationsCommandService
    {
        /// <summary>
        /// Sets the last known live notification.
        /// </summary>
        /// <param name="id">The ID of the last known notification.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task SetLastKnownAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Marks all notifications as read.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task MarkAllReadAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Marks specified notifications as read.
        /// </summary>
        /// <param name="ids">The notification IDs.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException">Value cannot be an empty collection.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task MarkReadAsync(ICollection<string> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Marks specified notifications as unread.
        /// </summary>
        /// <param name="ids">The notification IDs.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException">Value cannot be an empty collection.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task MarkUnreadAsync(ICollection<string> ids, CancellationToken cancellationToken = default);
    }
}
