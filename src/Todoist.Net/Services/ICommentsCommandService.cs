using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for comments management which can be executed in a transaction.
    /// </summary>
    public interface ICommentsCommandService
    {
        /// <summary>
        /// Adds the comment to a task asynchronous.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="taskId">The task identifier.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The temporary ID of the comment.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="comment" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ComplexId> AddToTaskAsync(Comment comment, ComplexId taskId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds the comment to a project asynchronous.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="projectId">The project ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The comment ID.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="comment" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ComplexId> AddToProjectAsync(Comment comment, ComplexId projectId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the comment asynchronous.
        /// </summary>
        /// <param name="id">The id of the comment.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the comment asynchronous.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="comment" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task UpdateAsync(Comment comment, CancellationToken cancellationToken = default);
    }
}
