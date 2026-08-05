using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal class WorkspaceFiltersService : WorkspaceFiltersCommandService, IWorkspaceFiltersService
    {
        internal WorkspaceFiltersService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<SyncResponse<WorkspaceFilterInfo>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncResourceAsync(ResourceType.WorkspaceFilters, r => r.WorkspaceFilters, syncToken, cancellationToken);
        }
    }
}
