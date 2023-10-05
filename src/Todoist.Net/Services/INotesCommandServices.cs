using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for notes management which can be executes in a transaction.
    /// </summary>
    public interface INotesCommandServices
    {
        /// <summary>
        /// Adds the note asynchronous.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The temporary ID of the note.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="note" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ComplexId> AddToItemAsync(Note note, ComplexId itemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds the note asynchronous.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <param name="projectId">The project ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The note ID.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="note" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ComplexId> AddToProjectAsync(Note note, ComplexId projectId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the note asynchronous.
        /// </summary>
        /// <param name="id">The id of the note.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the note asynchronous.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="note" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task UpdateAsync(Note note, CancellationToken cancellationToken = default);
    }
}
