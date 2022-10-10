using System;
using System.Collections.Generic;
using System.Net.Http;
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

        /// <summary>
        /// Adds a new task to a project asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The item ID.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="item" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>By default the task is added to the user’s Inbox project.</remarks>
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

        /// <summary>
        /// Closes a task asynchronous.
        /// </summary>
        /// <param name="id">The item ID.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>
        /// A simplified version of item_complete / item_update_date_complete.
        /// The command does exactly what official clients do when you close a task: regular task is completed and moved to history,
        /// subtask is checked (marked as done, but not moved to history), recurring task is moved forward (due date is updated).
        /// </remarks>
        public Task CloseAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.CloseItem, id);
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Completes tasks and optionally move them to history. See also <see cref="CloseAsync" /> for a simplified version of the command.
        /// </summary>
        /// <param name="completeItemArgument">The complete item argument.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="completeItemArgument"/> is <see langword="null"/></exception>
        public Task CompleteAsync(CompleteItemArgument completeItemArgument)
        {
            if (completeItemArgument == null)
            {
                throw new ArgumentNullException(nameof(completeItemArgument));
            }

            var command = new Command(CommandType.CompleteItem, completeItemArgument);

            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Completes a recurring task. See also <see cref="CloseAsync" /> for a simplified version of the command.
        /// </summary>
        /// <param name="id">The recurring task ID.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        public Task CompleteRecurringAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.CompleteRecurringItem, id);
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Completes a recurring task. See also <see cref="CloseAsync" /> for a simplified version of the command.
        /// </summary>
        /// <param name="completeRecurringItemArgument">The complete recurring item argument.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="completeRecurringItemArgument"/> is <see langword="null"/></exception>
        public Task CompleteRecurringAsync(CompleteRecurringItemArgument completeRecurringItemArgument)
        {
            if (completeRecurringItemArgument == null)
            {
                throw new ArgumentNullException(nameof(completeRecurringItemArgument));
            }

            var command = new Command(CommandType.CompleteRecurringItem, completeRecurringItemArgument);

            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Deletes an existing task asynchronous.
        /// </summary>
        /// <param name="id">Id of the task to delete.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task DeleteAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.DeleteItem, id);

            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Uncompletes an unarchived item and all its ancestors.
        /// </summary>
        /// <param name="id">The ids of the tasks.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        public Task UncompleteAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.UncompleteItem, id);

            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Updates a task asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task UpdateAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var command = new Command(CommandType.UpdateItem, item);
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Updates the day orders of multiple items at once.
        /// </summary>
        /// <param name="idsToOrder">The ids to order.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="idsToOrder"/> is <see langword="null"/></exception>
        public Task UpdateDayOrdersAsync(params OrderEntry[] idsToOrder)
        {
            if (idsToOrder == null)
            {
                throw new ArgumentNullException(nameof(idsToOrder));
            }

            var command = new Command(CommandType.UpdateDayOrderItem, new IdToOrderArgument(idsToOrder));
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Moves task to a different location asynchronous.
        /// </summary>
        /// <param name="moveArgument">The move entry.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="moveArgument" /> is <see langword="null" /></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        public Task MoveAsync(ItemMoveArgument moveArgument)
        {
            if (moveArgument == null)
            {
                throw new ArgumentNullException(nameof(moveArgument));
            }

            return ExecuteCommandAsync(new Command(CommandType.MoveItem, moveArgument));
        }

        /// <summary>
        /// Update the orders and indents of multiple items at once asynchronous.
        /// </summary>
        /// <param name="reorderEntries">The reorder entries.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reorderEntries" /> is <see langword="null" /></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="T:System.ArgumentException">Value cannot be an empty collection.</exception>
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
