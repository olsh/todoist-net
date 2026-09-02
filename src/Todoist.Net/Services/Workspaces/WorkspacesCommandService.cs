using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for workspace management which can be executed in a transaction.
    /// </summary>
    internal class WorkspacesCommandService : CommandServiceBase, IWorkspacesCommandService
    {
        internal WorkspacesCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal WorkspacesCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <inheritdoc/>
        public Task<ComplexId> AddAsync(AddWorkspace workspace, CancellationToken cancellationToken = default)
        {
            return ExecuteAddCommandAsync(CommandType.AddWorkspace, workspace, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(UpdateWorkspace workspace, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateWorkspace, workspace, cancellationToken);
        }

        /// <inheritdoc/>
        public Task LeaveAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return ExecuteEntityCommandAsync(CommandType.LeaveWorkspace, id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return ExecuteEntityCommandAsync(CommandType.DeleteWorkspace, id, cancellationToken);
        }


        /// <inheritdoc/>
        public Task ChangeUserRoleAsync(ChangeWorkspaceUserRoleArgument userRoleArgs, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateWorkspaceUser, userRoleArgs, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateProjectSortPreferenceAsync(UpdateProjectSortPreferenceArgument projectSortArgs, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateWorkspaceUserProjectSortPreference, projectSortArgs, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteUserAsync(DeleteWorkspaceUserArgument userArgs, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.DeleteWorkspaceUser, userArgs, cancellationToken);
        }

        /// <inheritdoc/>
        public Task InviteUsersAsync(InviteWorkspaceUsersArgument userArgs, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.InviteWorkspaceUsers, userArgs, cancellationToken);
        }


        /// <inheritdoc/>
        public Task<ComplexId> AddFolderAsync(ComplexId workspaceId, WorkspaceFolder folder, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNull(folder, nameof(folder));

            folder.WorkspaceId = workspaceId;

            return ExecuteAddCommandAsync(CommandType.AddWorkspaceFolder, folder, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateFolderAsync(ComplexId id, ComplexId workspaceId, WorkspaceFolder folder, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNull(folder, nameof(folder));

            folder.Id = id;
            folder.WorkspaceId = workspaceId;

            return ExecuteCommandAsync(CommandType.UpdateWorkspaceFolder, folder, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteFolderAsync(ComplexId id, ComplexId workspaceId, CancellationToken cancellationToken = default)
        {
            var folder = new WorkspaceFolder
            {
                Id = id,
                WorkspaceId = workspaceId
            };

            return ExecuteCommandAsync(CommandType.DeleteWorkspaceFolder, folder, cancellationToken);
        }
    }
}
