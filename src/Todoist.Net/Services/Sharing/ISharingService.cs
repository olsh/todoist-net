using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for sharing management.
    /// </summary>
    public interface ISharingService : ISharingCommandService
    {
        /// <summary>
        /// Gets a read-only collection of collaborators that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all collaborators and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of collaborators that were synchronized.
        /// </returns>
        Task<CollaboratorsSyncResponse> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default);
    }
}
