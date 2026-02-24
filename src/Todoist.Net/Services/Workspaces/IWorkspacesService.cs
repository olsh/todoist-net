using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist workspace APIs.
    /// </summary>
    public interface IWorkspacesService
    {
        /// <summary>
        /// Gets a read-only collection of workspaces that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all workspaces and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of workspaces that were synchronized.
        /// </returns>
        Task<SyncResponse<WorkspaceInfo>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a read-only collection of workspace users that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all workspace users and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of workspace users that were synchronized.
        /// </returns>
        Task<SyncResponse<WorkspaceUser>> SyncUsersAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a read-only collection of workspace folders that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all workspace folders and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of workspace folders that were synchronized.
        /// </returns>
        Task<SyncResponse<WorkspaceFolder>> SyncFoldersAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets pending invitation emails for a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace identifier.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The pending invitation emails.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IReadOnlyCollection<string>> GetInvitationsAsync(long workspaceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets pending invitation details for a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace identifier.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The pending invitations.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IReadOnlyCollection<WorkspaceInvitation>> GetInvitationDetailsAsync(long workspaceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Accepts a workspace invitation.
        /// </summary>
        /// <param name="inviteCode">The invitation code.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The invitation details.</returns>
        /// <exception cref="ArgumentException"><paramref name="inviteCode"/> is null or empty.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<WorkspaceInvitation> AcceptInvitationAsync(string inviteCode, CancellationToken cancellationToken = default);

        /// <summary>
        /// Rejects a workspace invitation.
        /// </summary>
        /// <param name="inviteCode">The invitation code.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The invitation details.</returns>
        /// <exception cref="ArgumentException"><paramref name="inviteCode"/> is null or empty.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<WorkspaceInvitation> RejectInvitationAsync(string inviteCode, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a pending workspace invitation.
        /// </summary>
        /// <param name="workspaceId">The workspace identifier.</param>
        /// <param name="userEmail">The invited user email.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The deleted invitation details.</returns>
        /// <exception cref="ArgumentException"><paramref name="userEmail"/> is null or empty.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<WorkspaceInvitation> DeleteInvitationAsync(long workspaceId, string userEmail, CancellationToken cancellationToken = default);

        /// <summary>
        /// Joins a workspace by invitation code or by workspace ID when domain auto-join is available.
        /// </summary>
        /// <param name="inviteCode">The invitation code.</param>
        /// <param name="workspaceId">The workspace identifier for auto-join by domain.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The workspace join result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="inviteCode"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Neither invite code nor workspace ID is set.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<WorkspaceJoinResult> JoinAsync(string inviteCode, long workspaceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets workspace users with optional workspace/pagination filtering.
        /// </summary>
        /// <param name="query">The workspace users query parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The paginated workspace users response.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedWorkspaceUsers> GetUsersAsync(WorkspaceUsersQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets active workspace projects with optional cursor/limit pagination.
        /// </summary>
        /// <param name="workspaceId">The workspace identifier.</param>
        /// <param name="query">Optional pagination query parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The paginated active workspace projects response.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedWorkspaceProjects> GetActiveProjectsAsync(long workspaceId, PaginationQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets archived workspace projects with optional cursor/limit pagination.
        /// </summary>
        /// <param name="workspaceId">The workspace identifier.</param>
        /// <param name="query">Optional pagination query parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The paginated archived workspace projects response.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedWorkspaceProjects> GetArchivedProjectsAsync(long workspaceId, PaginationQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets workspace plan details and usage information.
        /// </summary>
        /// <param name="workspaceId">The workspace identifier.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The workspace plan details.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<WorkspacePlanDetails> GetPlanDetailsAsync(long workspaceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Uploads and sets a workspace logo.
        /// </summary>
        /// <param name="workspaceId">The workspace identifier.</param>
        /// <param name="fileContent">The file content.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ArgumentNullException"><paramref name="fileContent"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task UpdateLogoAsync(long workspaceId, FileContent fileContent, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Deletes a workspace logo.
        /// </summary>
        /// <param name="workspaceId">The workspace identifier.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteLogoAsync(long workspaceId, CancellationToken cancellationToken = default);
    }
}
