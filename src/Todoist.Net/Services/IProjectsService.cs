using System.Collections.Generic;
using System.Net.Http;
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
        /// <returns>
        /// The archived projects.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Project>> GetArchivedAsync();

        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <returns>The projects.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Project>> GetAsync();

        /// <summary>
        /// Gets project by ID.
        /// </summary>
        /// <param name="id">The ID of the project.</param>
        /// <returns>
        /// The project.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ProjectInfo> GetAsync(ComplexId id);

        /// <summary>
        /// Gets a project’s uncompleted items.
        /// </summary>
        /// <param name="id">The ID of the project.</param>
        /// <returns>
        /// The project data.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ProjectData> GetDataAsync(ComplexId id);
    }
}
