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
        /// Gets all comments.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The comments.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<CommentsInfo> GetAsync(CancellationToken cancellationToken = default);
    }
}
