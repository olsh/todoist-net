using System;
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
        public async Task<ComplexId> AddAsync(AddTask task, CancellationToken cancellationToken = default)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var command = CreateAddCommand(CommandType.AddTask, task);
            await ExecuteCommandAsync(command, cancellationToken).ConfigureAwait(false);

            return task.Id;
        }

        /// <inheritdoc/>
        public Task CloseAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.CloseTask, id);
            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task CompleteAsync(CompleteTaskArgument completeTaskArgument, CancellationToken cancellationToken = default)
        {
            if (completeTaskArgument == null)
            {
                throw new ArgumentNullException(nameof(completeTaskArgument));
            }

            var command = new Command(CommandType.CompleteTask, completeTaskArgument);

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task CompleteRecurringAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.CompleteRecurringTask, id);
            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task CompleteRecurringAsync(CompleteRecurringTaskArgument completeRecurringTaskArgument, CancellationToken cancellationToken = default)
        {
            if (completeRecurringTaskArgument == null)
            {
                throw new ArgumentNullException(nameof(completeRecurringTaskArgument));
            }

            var command = new Command(CommandType.CompleteRecurringTask, completeRecurringTaskArgument);

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.DeleteTask, id);

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UncompleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.UncompleteTask, id);

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(UpdateTask task, CancellationToken cancellationToken = default)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var command = new Command(CommandType.UpdateTask, task);
            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateDayOrdersAsync(params OrderEntry[] idsToOrder) => UpdateDayOrdersAsync(CancellationToken.None, idsToOrder);

        /// <inheritdoc/>
        public Task UpdateDayOrdersAsync(CancellationToken cancellationToken, params OrderEntry[] idsToOrder)
        {
            if (idsToOrder == null)
            {
                throw new ArgumentNullException(nameof(idsToOrder));
            }

            var command = new Command(CommandType.UpdateDayOrderTask, new IdToOrderArgument(idsToOrder));
            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task MoveAsync(TaskMoveArgument moveArgument, CancellationToken cancellationToken = default)
        {
            if (moveArgument == null)
            {
                throw new ArgumentNullException(nameof(moveArgument));
            }

            return ExecuteCommandAsync(new Command(CommandType.MoveTask, moveArgument), cancellationToken);
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

            return ExecuteCommandAsync(new Command(CommandType.ReorderTasks, new ReorderTasksArgument(reorderEntries)), cancellationToken);
        }
    }
}
