using System;
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
    /// <seealso cref="Todoist.Net.Services.IProjectCommandService" />
    internal class ProjectsCommandService : CommandServiceBase, IProjectCommandService
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
        public async Task<ComplexId> AddAsync(Project project, CancellationToken cancellationToken = default)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            var command = CreateAddCommand(CommandType.AddProject, project);
            await ExecuteCommandAsync(command, cancellationToken).ConfigureAwait(false);

            return project.Id;
        }

        /// <inheritdoc/>
        public Task ArchiveAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.ArchiveProject, id);

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.DeleteProject, id);

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UnarchiveAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.UnarchiveProject, id);

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Project project, CancellationToken cancellationToken = default)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            var command = new Command(CommandType.UpdateProject, project);

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task MoveAsync(MoveArgument moveArgument, CancellationToken cancellationToken = default)
        {
            if (moveArgument == null)
            {
                throw new ArgumentNullException(nameof(moveArgument));
            }

            return ExecuteCommandAsync(new Command(CommandType.MoveProject, moveArgument), cancellationToken);
        }

        /// <inheritdoc/>
        public Task ReorderAsync(params ReorderEntry[] reorderEntries) => ReorderAsync(CancellationToken.None, reorderEntries);

        /// <inheritdoc/>
        public Task ReorderAsync(CancellationToken cancellationToken, params ReorderEntry[] reorderEntries)
        {
            if (reorderEntries == null)
            {
                throw new ArgumentNullException(nameof(reorderEntries));
            }

            if (reorderEntries.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(reorderEntries));
            }

            return ExecuteCommandAsync(new Command(CommandType.ReorderProjects, new ReorderProjectsArgument(reorderEntries)), cancellationToken);
        }
    }
}
