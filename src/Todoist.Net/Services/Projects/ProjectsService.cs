using System.Collections.Generic;
using System.Threading;
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
        public Task<PaginatedResponse<Project>> GetArchivedAsync(CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedResponse<Project>>(
                "projects/archived",
                new List<KeyValuePair<string, string>>(),
                cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Project>> GetAsync(CancellationToken cancellationToken = default)
        {
            var response = await TodoistClient.GetResourcesAsync(cancellationToken, ResourceType.Projects).ConfigureAwait(false);

            return response.Projects;
        }

        /// <inheritdoc/>
        public Task<Project> GetAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<Project>(
                $"projects/{id}",
                new List<KeyValuePair<string, string>>(),
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ProjectData> GetDataAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<ProjectData>(
                $"projects/{id}/full",
                new List<KeyValuePair<string, string>>(),
                cancellationToken);
        }
    }
}
