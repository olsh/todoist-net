using System;
using System.Collections.Generic;
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
        public async Task<ComplexId> AddAsync(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            var command = CreateAddCommand(CommandType.AddProject, project);
            await ExecuteCommandAsync(command).ConfigureAwait(false);

            return project.Id;
        }

        /// <inheritdoc/>
        public Task ArchiveAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.ArchiveProject, id);

            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.DeleteProject, id);

            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task UnarchiveAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.UnarchiveProject, id);

            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            var command = new Command(CommandType.UpdateProject, project);

            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task MoveAsync(MoveArgument moveArgument)
        {
            if (moveArgument == null)
            {
                throw new ArgumentNullException(nameof(moveArgument));
            }

            return ExecuteCommandAsync(new Command(CommandType.MoveProject, moveArgument));
        }

        /// <inheritdoc/>
        public Task ReorderAsync(params ReorderEntry[] reorderEntries)
        {
            if (reorderEntries == null)
            {
                throw new ArgumentNullException(nameof(reorderEntries));
            }

            if (reorderEntries.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(reorderEntries));
            }

            return ExecuteCommandAsync(new Command(CommandType.ReorderProjects, new ReorderProjectsArgument(reorderEntries)));
        }
    }
}
