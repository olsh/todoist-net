using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for projects management which can be executed in a transaction.
    /// </summary>
    /// <seealso cref="CommandServiceBase" />
    /// <seealso cref="Todoist.Net.Services.IProjectsCommandService" />
    internal class ProjectsCommandService : CommandServiceBase, IProjectsCommandService
    {
        internal ProjectsCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal ProjectsCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <inheritdoc/>
        public Task<ComplexId> AddAsync(AddProject project, CancellationToken cancellationToken = default)
        {
            return ExecuteAddCommandAsync(CommandType.AddProject, project, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(UpdateProject project, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateProject, project, cancellationToken);
        }

        /// <inheritdoc/>
        public Task MoveAsync(MoveArgument moveArgument, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.MoveProject, moveArgument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task MoveToWorkspaceAsync(MoveProjectToWorkspaceArgument moveArgument, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.MoveProjectToWorkspace, moveArgument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task MoveToPersonalAsync(ComplexId projectId, CancellationToken cancellationToken = default)
        {
            var moveArgument = new ProjectIdArgument(projectId);
            return ExecuteCommandAsync(CommandType.MoveProjectToPersonal, moveArgument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task LeaveAsync(ComplexId projectId, CancellationToken cancellationToken = default)
        {
            var leaveArgument = new ProjectIdArgument(projectId);
            return ExecuteCommandAsync(CommandType.LeaveProject, leaveArgument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return ExecuteEntityCommandAsync(CommandType.DeleteProject, id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task ArchiveAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return ExecuteEntityCommandAsync(CommandType.ArchiveProject, id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UnarchiveAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return ExecuteEntityCommandAsync(CommandType.UnarchiveProject, id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task ReorderAsync(ReorderProjectsArgument reorderArgument, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.ReorderProjects, reorderArgument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task ChangeRoleAsync(ChangeProjectRoleArgument argument, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.ChangeProjectRole, argument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task SetViewOptionsDefaultsAsync(ProjectViewOptionsDefaults viewDefaults, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.SetProjectViewOptionsDefaults, viewDefaults, cancellationToken);
        }
    }
}
