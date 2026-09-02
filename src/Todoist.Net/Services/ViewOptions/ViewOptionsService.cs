using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for view options.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.ViewOptionsCommandService" />
    /// <seealso cref="Todoist.Net.Services.IViewOptionsService" />
    internal class ViewOptionsService : ViewOptionsCommandService, IViewOptionsService
    {
        internal ViewOptionsService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<SyncResponse<ViewOptions>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncResourceAsync(ResourceType.ViewOptions, r => r.ViewOptions, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<SyncResponse<ProjectViewOptionsDefaults>> SyncProjectDefaultsAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncResourceAsync(ResourceType.ProjectViewOptionsDefaults, r => r.ProjectViewOptionsDefaults, syncToken, cancellationToken);
        }
    }
}
