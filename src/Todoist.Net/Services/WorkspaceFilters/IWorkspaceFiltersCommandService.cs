using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for workspace filters management which can be executed in a transaction.
    /// </summary>
    public interface IWorkspaceFiltersCommandService
    {
        /// <summary>
        /// Adds a new workspace filter.
        /// </summary>
        /// <param name="workspaceFilter">The workspace filter payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The temporary ID of the workspace filter.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="workspaceFilter"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task<ComplexId> AddAsync(AddWorkspaceFilter workspaceFilter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing workspace filter.
        /// </summary>
        /// <param name="workspaceFilter">The workspace filter payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="workspaceFilter"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task UpdateAsync(UpdateWorkspaceFilter workspaceFilter, CancellationToken cancellationToken = default);
    
        /// <summary>
        /// Deletes a workspace filter.
        /// </summary>
        /// <param name="id">The workspace filter identifier.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the order of workspace filters.
        /// </summary>
        /// <param name="workspaceFilterOrders">The workspace filter orders.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task UpdateOrdersAsync(UpdateWorkspaceFilterOrders workspaceFilterOrders, CancellationToken cancellationToken = default);
    }
}
