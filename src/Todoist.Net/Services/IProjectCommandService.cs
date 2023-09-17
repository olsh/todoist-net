using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for projects management which can be executed in a transaction.
    /// </summary>
    public interface IProjectCommandService
    {
        /// <summary>
        /// Adds a new project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The temporary ID of the project.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="project"/> is <see langword="null"/></exception>
        Task<ComplexId> AddAsync(Project project, CancellationToken cancellationToken = default);

        /// <summary>
        /// Archive project and its children.
        /// </summary>
        /// <param name="id">The ids.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/></exception>
        /// <remarks>Only available for Todoist Premium users.</remarks>
        Task ArchiveAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete an existing project and all its descendants.
        /// </summary>
        /// <param name="id">The project ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns> Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/></exception>
        Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Un archive project and its children.
        /// </summary>
        /// <param name="id">The ids.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns> Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/></exception>
        /// <remarks>Only available for Todoist Premium users.</remarks>
        Task UnarchiveAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="project"/> is <see langword="null"/></exception>
        Task UpdateAsync(Project project, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates parent project relationships of the project asynchronous.
        /// </summary>
        /// <param name="moveArgument">The move entry.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="moveArgument" /> is <see langword="null" /></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task MoveAsync(MoveArgument moveArgument, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update the orders and indents of multiple projects at once asynchronous.
        /// </summary>
        /// <param name="reorderEntries">The reorder entries.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reorderEntries" /> is <see langword="null" /></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="T:System.ArgumentException">Value cannot be an empty collection.</exception>
        Task ReorderAsync(params ReorderEntry[] reorderEntries);

        /// <summary>
        /// Update the orders and indents of multiple projects at once asynchronous.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <param name="reorderEntries">The reorder entries.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reorderEntries" /> is <see langword="null" /></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="T:System.ArgumentException">Value cannot be an empty collection.</exception>
        Task ReorderAsync(CancellationToken cancellationToken, params ReorderEntry[] reorderEntries);
    }
}
