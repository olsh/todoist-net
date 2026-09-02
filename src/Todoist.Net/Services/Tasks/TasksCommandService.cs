using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for Todoist tasks management which can be executed in a transaction.
    /// </summary>
    /// <seealso cref="CommandServiceBase" />
    /// <seealso cref="Todoist.Net.Services.ITasksCommandService" />
    internal class TasksCommandService : CommandServiceBase, ITasksCommandService
    {
        internal TasksCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal TasksCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <inheritdoc/>
        public Task<ComplexId> AddAsync(AddTask task, CancellationToken cancellationToken = default)
        {
            return ExecuteAddCommandAsync(CommandType.AddTask, task, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(UpdateTask task, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateTask, task, cancellationToken);
        }

        /// <inheritdoc/>
        public Task MoveAsync(MoveTaskArgument moveArgument, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.MoveTask, moveArgument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task ReorderAsync(ReorderTasksArgument reorderArgument, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.ReorderTasks, reorderArgument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return ExecuteEntityCommandAsync(CommandType.DeleteTask, id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task CloseAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return ExecuteEntityCommandAsync(CommandType.CloseTask, id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task CompleteRecurringAsync(CompleteRecurringTaskArgument completeArgument, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.CompleteRecurringTask, completeArgument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task CompleteAsync(CompleteTaskArgument completeArgument, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.CompleteTask, completeArgument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UncompleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return ExecuteEntityCommandAsync(CommandType.UncompleteTask, id, cancellationToken);
        }
    }
}
