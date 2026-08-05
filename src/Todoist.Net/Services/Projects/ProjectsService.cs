using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
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
        public Task<SyncResponse<ProjectInfo>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncResourceAsync(ResourceType.Projects, r => r.Projects, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<SyncResponse<ProjectViewOptionsDefaults>> SyncViewOptionsDefaultsAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncResourceAsync(ResourceType.ProjectViewOptionsDefaults, r => r.ProjectViewOptionsDefaults, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<ProjectInfo>> SearchAsync(PaginatedSearchQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedResponse<ProjectInfo>>("projects/search", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<ProjectInfo>> GetArchivedAsync(PaginationQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedResponse<ProjectInfo>>("projects/archived", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<ProjectCollaborator>> GetCollaboratorsAsync(string id, PaginationQuery query = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(id, nameof(id));

            return TodoistClient.GetAsync<PaginatedResponse<ProjectCollaborator>>($"projects/{id}/collaborators", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ProjectPermissions> GetPermissionsAsync(CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<ProjectPermissions>("projects/permissions", cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<ProjectInfo>> GetAsync(PaginationQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedResponse<ProjectInfo>>("projects", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ProjectInfo> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(id, nameof(id));

            return TodoistClient.GetAsync<ProjectInfo>($"projects/{id}", cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ProjectData> GetDataAsync(string id, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(id, nameof(id));

            return TodoistClient.GetAsync<ProjectData>($"projects/{id}/full", cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ProjectData> JoinAsync(string id, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(id, nameof(id));

            return TodoistClient.PostAsync<ProjectData>($"projects/{id}/join", cancellationToken: cancellationToken);
        }
    }
}
