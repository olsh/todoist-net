using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;
using Todoist.Net.Models.Types;

namespace Todoist.Net.Services
{
    public interface IProjectService : IProjectCommandService
    {
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
    }
}