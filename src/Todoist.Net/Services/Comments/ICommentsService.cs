using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for comments management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.ICommentsCommandService" />
    public interface ICommentsService : ICommentsCommandService
    {
        /// <summary>
        /// Gets a read-only collection of comments that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all comments and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of comments that were synchronized.
        /// </returns>
        Task<CommentsSyncResponse> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets comments with cursor/limit pagination.
        /// </summary>
        /// <param name="query">The pagination query.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The comments.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedResponse<Comment>> GetAsync(CommentsPaginationQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a comment by ID.
        /// </summary>
        /// <param name="id">The ID of the comment.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The comment.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<Comment> GetAsync(string id, CancellationToken cancellationToken = default);
    }
}
