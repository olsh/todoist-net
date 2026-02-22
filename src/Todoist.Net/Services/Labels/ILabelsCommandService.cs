using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for labels management which can be executes in a transaction.
    /// </summary>
    public interface ILabelsCommandService
    {
        /// <summary>
        /// Adds a label asynchronous.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The temporary ID of the label.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="label" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ComplexId> AddAsync(Label label, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a label asynchronous.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="label"/> is <see langword="null"/></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task UpdateAsync(Label label, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an existing label asynchronous.
        /// </summary>
        /// <param name="id">The ID of the label.</param>
        /// <param name="keepAsShared">A value indicating whether to keep the label continue to appear on tasks as a shared label.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(ComplexId id, bool keepAsShared = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a shared label by name asynchronous.
        /// </summary>
        /// <param name="name">The shared label name.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Value cannot be null or empty.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteSharedAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Renames a shared label asynchronous.
        /// </summary>
        /// <param name="name">The current shared label name.</param>
        /// <param name="newName">The new shared label name.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Value cannot be null or empty.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task RenameSharedAsync(string name, string newName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update the orders of multiple labels at once.
        /// </summary>
        /// <param name="orderMapping">The order mapping argument.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="orderMapping"/> is <see langword="null"/></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task UpdateOrderAsync(IdToOrderMappingArgument orderMapping, CancellationToken cancellationToken = default);
    }
}
