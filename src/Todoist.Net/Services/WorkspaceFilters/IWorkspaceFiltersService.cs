using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist workspace filters APIs.
    /// </summary>
    public interface IWorkspaceFiltersService
    {
        /// <summary>
        /// Gets a read-only collection of workspace filters that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all workspace filters and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of workspace filters that were synchronized.
        /// </returns>
        Task<SyncResponse<WorkspaceFilterInfo>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default);
    }
}
