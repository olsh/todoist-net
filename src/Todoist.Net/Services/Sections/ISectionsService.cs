using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for sections management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.ISectionsCommandService" />
    public interface ISectionsService : ISectionsCommandService
    {
        /// <summary>
        /// Gets a read-only collection of sections that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all sections and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of sections that were synchronized.
        /// </returns>
        Task<SyncResponse<Section>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets sections based on a search query with cursor/limit pagination.
        /// </summary>
        /// <param name="query">The pagination query.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The sections.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedResponse<Section>> SearchAsync(SectionsSearchQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets sections with cursor/limit pagination.
        /// </summary>
        /// <param name="query">The pagination query.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The sections.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedResponse<Section>> GetAsync(SectionsPaginationQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets section by ID.
        /// </summary>
        /// <param name="id">The ID of the section.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The section.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<Section> GetAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new section and returns it.
        /// </summary>
        /// <param name="section">The section payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The created section.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<Section> AddAndReturnAsync(AddSection section, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing section and returns it.
        /// </summary>
        /// <param name="id">The ID of the section to update.</param>
        /// <param name="section">The section payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated section.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<Section> UpdateAndReturnAsync(string id, UpdateSection section, CancellationToken cancellationToken = default);
    }
}
