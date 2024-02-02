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
        public async Task<IEnumerable<Collaborator>> GetCollaboratorsAsync(CancellationToken cancellationToken = default)
        {
            var response = await TodoistClient.GetResourcesAsync(cancellationToken, ResourceType.Collaborators).ConfigureAwait(false);

            return response.Collaborators;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CollaboratorState>> GetCollaboratorStatesAsync(CancellationToken cancellationToken = default)
        {
            var response = await TodoistClient.GetResourcesAsync(cancellationToken, ResourceType.Collaborators).ConfigureAwait(false);

            return response.CollaboratorStates;
        }
    }
}
