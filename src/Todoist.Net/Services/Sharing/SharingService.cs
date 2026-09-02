using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for sharing management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.SharingCommandService" />
    internal class SharingService : SharingCommandService, ISharingService
    {
        public SharingService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<CollaboratorsSyncResponse> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return TodoistClient.SyncResourcesAsync<CollaboratorsSyncResponse>(new[] { ResourceType.Collaborators }, syncToken, cancellationToken);
        }
    }
}
