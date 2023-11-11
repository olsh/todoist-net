using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for collaborators management.
    /// </summary>
    internal class CollaboratorsService : ICollaboratorsService
    {
        private readonly IAdvancedTodoistClient _todoistClient;

        internal CollaboratorsService(IAdvancedTodoistClient todoistClient)
        {
            _todoistClient = todoistClient;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Collaborator>> GetAsync(CancellationToken cancellationToken = default)
        {
            var response = await _todoistClient.GetResourcesAsync(cancellationToken, ResourceType.Collaborators).ConfigureAwait(false);

            return response.Collaborators;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CollaboratorState>> GetStatesAsync(CancellationToken cancellationToken = default)
        {
            var response = await _todoistClient.GetResourcesAsync(cancellationToken, ResourceType.Collaborators).ConfigureAwait(false);

            return response.CollaboratorStates;
        }
    }
}
