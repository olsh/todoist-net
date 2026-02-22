using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for labels management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.ILabelsCommandService" />
    public interface ILabelsService : ILabelsCommandService
    {
        /// <summary>
        /// Gets a read-only collection of labels that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all labels and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of labels that were synchronized.
        /// </returns>
        Task<SyncResponse<Label>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets labels based on a search query with cursor/limit pagination.
        /// </summary>
        /// <param name="query">The pagination query.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The labels.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedResponse<Label>> SearchAsync(PaginatedSearchQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets labels with cursor/limit pagination.
        /// </summary>
        /// <param name="query">The pagination query.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The labels.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedResponse<Label>> GetAsync(PaginationQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a set of unique strings containing labels from active tasks with cursor/limit pagination.
        /// </summary>
        /// <param name="query">The pagination query.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <remarks>
        /// By default, the names of a user's personal labels will also be included. 
        /// These can be excluded by passing the <see cref="SharedLabelsPaginationQuery.OmitPersonal"/> query parameter as <see langword="true"/>.
        /// </remarks>
        /// <returns>The shared labels.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedResponse<string>> GetSharedAsync(SharedLabelsPaginationQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets label by ID.
        /// </summary>
        /// <param name="id">The ID of the label.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The label.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<Label> GetAsync(string id, CancellationToken cancellationToken = default);
    
        /// <summary>
        /// Creates a new label and returns it.
        /// </summary>
        /// <param name="label">The label payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The created label.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<Label> AddAndReturnAsync(Label label, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing label and returns it.
        /// </summary>
        /// <param name="id">The ID of the label to update.</param>
        /// <param name="label">The label payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated label.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<Label> UpdateAndReturnAsync(string id, Label label, CancellationToken cancellationToken = default);
    }
}
