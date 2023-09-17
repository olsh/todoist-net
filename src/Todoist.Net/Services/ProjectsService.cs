using System.Collections.Generic;
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

        /// <inheritdoc/>
        public Task<IEnumerable<Project>> GetArchivedAsync()
        {
            return TodoistClient.PostAsync<IEnumerable<Project>>(
                "projects/get_archived",
                new List<KeyValuePair<string, string>>());
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Project>> GetAsync()
        {
            var response = await TodoistClient.GetResourcesAsync(ResourceType.Projects).ConfigureAwait(false);

            return response.Projects;
        }

        /// <inheritdoc/>
        public Task<ProjectInfo> GetAsync(ComplexId id)
        {
            return TodoistClient.PostAsync<ProjectInfo>(
                "projects/get",
                new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("project_id", id.ToString())
                    });
        }

        /// <inheritdoc/>
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
