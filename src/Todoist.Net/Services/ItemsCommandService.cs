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
    /// <seealso cref="Todoist.Net.Services.IItemsCommandService" />
    internal class ItemsCommandService : CommandServiceBase, IItemsCommandService
    {
        internal ItemsCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal ItemsCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <inheritdoc/>
        public async Task<ComplexId> AddAsync(AddItem item, CancellationToken cancellationToken = default)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var command = CreateAddCommand(CommandType.AddItem, item);
            await ExecuteCommandAsync(command, cancellationToken).ConfigureAwait(false);

            return item.Id;
        }

        /// <inheritdoc/>
        public Task CloseAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.CloseItem, id);
            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task CompleteAsync(CompleteItemArgument completeItemArgument, CancellationToken cancellationToken = default)
        {
            if (completeItemArgument == null)
            {
                throw new ArgumentNullException(nameof(completeItemArgument));
            }

            var command = new Command(CommandType.CompleteItem, completeItemArgument);

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task CompleteRecurringAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.CompleteRecurringItem, id);
            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task CompleteRecurringAsync(CompleteRecurringItemArgument completeRecurringItemArgument, CancellationToken cancellationToken = default)
        {
            if (completeRecurringItemArgument == null)
            {
                throw new ArgumentNullException(nameof(completeRecurringItemArgument));
            }

            var command = new Command(CommandType.CompleteRecurringItem, completeRecurringItemArgument);

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.DeleteItem, id);

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UncompleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.UncompleteItem, id);

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(UpdateItem item, CancellationToken cancellationToken = default)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var command = new Command(CommandType.UpdateItem, item);
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

            var command = new Command(CommandType.UpdateDayOrderItem, new IdToOrderArgument(idsToOrder));
            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task MoveAsync(ItemMoveArgument moveArgument, CancellationToken cancellationToken = default)
        {
            if (moveArgument == null)
            {
                throw new ArgumentNullException(nameof(moveArgument));
            }

            return ExecuteCommandAsync(new Command(CommandType.MoveItem, moveArgument), cancellationToken);
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

            return ExecuteCommandAsync(new Command(CommandType.ReorderItems, new ReorderItemsArgument(reorderEntries)), cancellationToken);
        }
    }
}
