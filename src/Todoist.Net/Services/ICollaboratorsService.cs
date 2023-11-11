using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist collaborators management.
    /// </summary>
    public interface ICollaboratorsService
    {
        /// <summary>
        /// Gets all collaborators.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The collaborators.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Collaborator>> GetAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all collaborator states.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The collaborator states.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<CollaboratorState>> GetStatesAsync(CancellationToken cancellationToken = default);
    }
}
