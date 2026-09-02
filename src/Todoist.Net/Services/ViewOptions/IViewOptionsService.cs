using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist view options APIs.
    /// </summary>
    public interface IViewOptionsService : IViewOptionsCommandService
    {
        /// <summary>
        /// Gets a read-only collection of view options that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all view options and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of view options that were synchronized.
        /// </returns>
        Task<SyncResponse<ViewOptions>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a read-only collection of project view options defaults that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all project view options defaults and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of project view options defaults that were synchronized.
        /// </returns>
        Task<SyncResponse<ProjectViewOptionsDefaults>> SyncProjectDefaultsAsync(string syncToken = "*", CancellationToken cancellationToken = default);
    }
}
