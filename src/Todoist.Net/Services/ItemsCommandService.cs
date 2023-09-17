using System;
using System.Collections.Generic;
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
        public async Task<ComplexId> AddAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var command = CreateAddCommand(CommandType.AddItem, item);
            await ExecuteCommandAsync(command).ConfigureAwait(false);

            return item.Id;
        }

        /// <inheritdoc/>
        public Task CloseAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.CloseItem, id);
            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task CompleteAsync(CompleteItemArgument completeItemArgument)
        {
            if (completeItemArgument == null)
            {
                throw new ArgumentNullException(nameof(completeItemArgument));
            }

            var command = new Command(CommandType.CompleteItem, completeItemArgument);

            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task CompleteRecurringAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.CompleteRecurringItem, id);
            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task CompleteRecurringAsync(CompleteRecurringItemArgument completeRecurringItemArgument)
        {
            if (completeRecurringItemArgument == null)
            {
                throw new ArgumentNullException(nameof(completeRecurringItemArgument));
            }

            var command = new Command(CommandType.CompleteRecurringItem, completeRecurringItemArgument);

            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.DeleteItem, id);

            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task UncompleteAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.UncompleteItem, id);

            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var command = new Command(CommandType.UpdateItem, item);
            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task UpdateDayOrdersAsync(params OrderEntry[] idsToOrder)
        {
            if (idsToOrder == null)
            {
                throw new ArgumentNullException(nameof(idsToOrder));
            }

            var command = new Command(CommandType.UpdateDayOrderItem, new IdToOrderArgument(idsToOrder));
            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task MoveAsync(ItemMoveArgument moveArgument)
        {
            if (moveArgument == null)
            {
                throw new ArgumentNullException(nameof(moveArgument));
            }

            return ExecuteCommandAsync(new Command(CommandType.MoveItem, moveArgument));
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

            return ExecuteCommandAsync(new Command(CommandType.ReorderItems, new ReorderItemsArgument(reorderEntries)));
        }
    }
}
