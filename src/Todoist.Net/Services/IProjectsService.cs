using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for projects management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.IProjectCommandService" />
    public interface IProjectsService : IProjectCommandService
    {
        /// <summary>
        /// Gets archived projects.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The archived projects.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Project>> GetArchivedAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The projects.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Project>> GetAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets project by ID.
        /// </summary>
        /// <param name="id">The ID of the project.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The project.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ProjectInfo> GetAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a project’s uncompleted items.
        /// </summary>
        /// <param name="id">The ID of the project.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The project data.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ProjectData> GetDataAsync(ComplexId id, CancellationToken cancellationToken = default);
    }
}
