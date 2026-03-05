using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal class WorkspacesService : WorkspacesCommandService, IWorkspacesService
    {
        internal WorkspacesService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<SyncResponse<WorkspaceInfo>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncResourceAsync(ResourceType.Workspaces, r => r.Workspaces, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<SyncResponse<WorkspaceUser>> SyncUsersAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncResourceAsync(ResourceType.WorkspaceUsers, r => r.WorkspaceUsers, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<SyncResponse<WorkspaceFolder>> SyncFoldersAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncResourceAsync(ResourceType.WorkspaceFolders, r => r.WorkspaceFolders, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<string>> GetInvitationsAsync(long workspaceId, CancellationToken cancellationToken = default)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "workspace_id", workspaceId.ToString() }
            };

            return TodoistClient.GetAsync<IReadOnlyCollection<string>>("workspaces/invitations", queryParams, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<WorkspaceInvitation>> GetInvitationDetailsAsync(long workspaceId, CancellationToken cancellationToken = default)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "workspace_id", workspaceId.ToString() }
            };

            return TodoistClient.GetAsync<IReadOnlyCollection<WorkspaceInvitation>>("workspaces/invitations/all", queryParams, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<WorkspaceInvitation> AcceptInvitationAsync(string inviteCode, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(inviteCode, nameof(inviteCode));

            return TodoistClient.PutAsync<WorkspaceInvitation>($"workspaces/invitations/{inviteCode}/accept", cancellationToken);
        }

        /// <inheritdoc/>
        public Task<WorkspaceInvitation> RejectInvitationAsync(string inviteCode, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(inviteCode, nameof(inviteCode));

            return TodoistClient.PutAsync<WorkspaceInvitation>($"workspaces/invitations/{inviteCode}/reject", cancellationToken);
        }

        /// <inheritdoc/>
        public Task<WorkspaceInvitation> DeleteInvitationAsync(long workspaceId, string userEmail, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(userEmail, nameof(userEmail));

            var request = new WorkspaceInvitationDeleteRequest(workspaceId, userEmail);
            
            return TodoistClient.PostJsonAsync<WorkspaceInvitationDeleteRequest, WorkspaceInvitation>(
                "workspaces/invitations/delete", request, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<WorkspaceJoinResult> JoinByCodeAsync(string inviteCode, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(inviteCode, nameof(inviteCode));

            var request = new WorkspaceJoinRequest
            {
                InviteCode = inviteCode
            };

            return TodoistClient.PostJsonAsync<WorkspaceJoinRequest, WorkspaceJoinResult>("workspaces/join", request, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<WorkspaceJoinResult> JoinByDomainAsync(long workspaceId, CancellationToken cancellationToken = default)
        {            
            var request = new WorkspaceJoinRequest
            {
                WorkspaceId = workspaceId
            };

            return TodoistClient.PostJsonAsync<WorkspaceJoinRequest, WorkspaceJoinResult>("workspaces/join", request, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedWorkspaceUsers> GetUsersAsync(WorkspaceUsersQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedWorkspaceUsers>(
                "workspaces/users", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedWorkspaceProjects> GetActiveProjectsAsync(long workspaceId, PaginationQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedWorkspaceProjects>(
                $"workspaces/{workspaceId}/projects/active", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedWorkspaceProjects> GetArchivedProjectsAsync(long workspaceId, PaginationQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedWorkspaceProjects>(
                $"workspaces/{workspaceId}/projects/archived", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<WorkspacePlanDetails> GetPlanDetailsAsync(long workspaceId, CancellationToken cancellationToken = default)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "workspace_id", workspaceId.ToString() }
            };

            return TodoistClient.GetAsync<WorkspacePlanDetails>("workspaces/plan_details", queryParams, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateLogoAsync(long workspaceId, UploadFile logo, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNull(logo, nameof(logo));
            
            var parameters = new Dictionary<string, string>
            {
                { "workspace_id", workspaceId.ToString() }
            };

            return TodoistClient.PostFilesAsync("workspaces/logo", new[] { logo }, parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteLogoAsync(long workspaceId, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                { "workspace_id", workspaceId.ToString() },
                { "delete", "true" }
            };
            return TodoistClient.PostAsync("workspaces/logo", parameters, cancellationToken);
        }
    }
}
