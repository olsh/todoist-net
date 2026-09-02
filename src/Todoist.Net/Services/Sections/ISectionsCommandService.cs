using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for sections management which can be executed in a transaction.
    /// </summary>
    public interface ISectionsCommandService
    {
        /// <summary>
        /// Add a new section to a project.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The ID of the section, or a temporary ID when executed in a transaction.</returns>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="section" /> is <see langword="null" /></exception>
        Task<ComplexId> AddAsync(AddSection section, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the section.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="section" /> is <see langword="null" /></exception>
        Task UpdateAsync(UpdateSection section, CancellationToken cancellationToken = default);

        /// <summary>
        /// Moves the section to a different project.
        /// </summary>
        /// <param name="moveArgument">The move argument.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="moveArgument" /> is <see langword="null" /></exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task MoveAsync(MoveSectionArgument moveArgument, CancellationToken cancellationToken = default);

        /// <summary>
        /// Reorders the sections.
        /// </summary>
        /// <param name="reorderArgument">The reorder argument.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reorderArgument" /> is <see langword="null" /></exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task ReorderAsync(ReorderSectionsArgument reorderArgument, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a section and all its descendants tasks.
        /// </summary>
        /// <param name="id">The section ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns> Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Archive a section and all its descendants tasks.
        /// </summary>
        /// <param name="id">The section ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task ArchiveAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Unarchive a section.
        /// </summary>
        /// <param name="id">The section ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns> Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task UnarchiveAsync(ComplexId id, CancellationToken cancellationToken = default);
    }
}
