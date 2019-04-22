using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for projects management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.ProjectsCommandService" />
    /// <seealso cref="Todoist.Net.Services.IProjectsService" />
    internal class ProjectsService : ProjectsCommandService, IProjectsService
    {
        internal ProjectsService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <summary>
        /// Gets archived projects.
        /// </summary>
        /// <returns>
        /// The archived projects.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task<IEnumerable<Project>> GetArchivedAsync()
        {
            return TodoistClient.PostAsync<IEnumerable<Project>>(
                "projects/get_archived",
                new List<KeyValuePair<string, string>>());
        }

        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <returns>The projects.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task<IEnumerable<Project>> GetAsync()
        {
            var response = await TodoistClient.GetResourcesAsync(ResourceType.Projects).ConfigureAwait(false);

            return response.Projects;
        }

        /// <summary>
        /// Gets a project by ID.
        /// </summary>
        /// <param name="id">The ID of the project.</param>
        /// <returns>
        /// The project.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task<ProjectInfo> GetAsync(ComplexId id)
        {
            return TodoistClient.PostAsync<ProjectInfo>(
                "projects/get",
                new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("project_id", id.ToString())
                    });
        }

        /// <summary>
        /// Gets a project’s uncompleted items.
        /// </summary>
        /// <param name="id">The ID of the project.</param>
        /// <returns>
        /// The project data.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task<ProjectData> GetDataAsync(ComplexId id)
        {
            return TodoistClient.PostAsync<ProjectData>(
                "projects/get_data",
                new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("project_id", id.ToString())
                    });
        }
    }
}
