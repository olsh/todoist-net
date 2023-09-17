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
        /// Deletes an existing label asynchronous.
        /// </summary>
        /// <param name="id">The ID of the label.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default);

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
        /// Update the orders of multiple labels at once.
        /// </summary>
        /// <param name="orderEntries">The order entries.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="orderEntries"/> is <see langword="null"/></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task UpdateOrderAsync(params OrderEntry[] orderEntries);

        /// <summary>
        /// Update the orders of multiple labels at once.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <param name="orderEntries">The order entries.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="orderEntries"/> is <see langword="null"/></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task UpdateOrderAsync(CancellationToken cancellationToken, params OrderEntry[] orderEntries);
    }
}
