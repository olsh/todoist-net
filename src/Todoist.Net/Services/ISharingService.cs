using System.Collections.Generic;
using System.Net.Http;
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
        /// Gets all collaborators.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The collaborators.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Collaborator>> GetCollaboratorsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all collaborator states.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The collaborator states.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<CollaboratorState>> GetCollaboratorStatesAsync(CancellationToken cancellationToken = default);
    }
}
