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
        /// <param name="forceHistory">Whether these tasks should be moved to history.</param>
        /// <param name="ids">The ids of the tasks.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        public Task CompleteAsync(bool forceHistory = true, params ComplexId[] ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var command = new Command(CommandType.CompleteItem, new CompleteItemsArgument(ids, forceHistory));
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
        /// <param name="recurringItemState">State of the recurring item.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="recurringItemState"/> is <see langword="null"/></exception>
        public Task CompleteRecurringAsync(RecurringItemState recurringItemState)
        {
            if (recurringItemState == null)
            {
                throw new ArgumentNullException(nameof(recurringItemState));
            }

            var command = new Command(
                              CommandType.CompleteRecurringItem,
                              new CompleteRecurringItemArgument(recurringItemState));
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Deletes an existing task asynchronous.
        /// </summary>
        /// <param name="ids">List of the IDs of the tasks to delete.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task DeleteAsync(params ComplexId[] ids)
        {
            var command = CreateCollectionCommand(CommandType.DeleteItem, ids);
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Moves the tasks to the project asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="items">The items.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="items" /> is <see langword="null" /></exception>
        /// <exception cref="ArgumentException">Unable to move an item with an empty source project ID.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        public Task MoveToProjectAsync(ComplexId projectId, params Item[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var command = new Command(CommandType.MoveItem, new MoveItemArgument(items, projectId));
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Uncompletes tasks and moves them to the active projects.
        /// </summary>
        /// <param name="ids">The ids of the tasks.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        public Task UncompleteAsync(params ComplexId[] ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var command = CreateCollectionCommand(CommandType.UncompleteItem, ids);
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Uncompletes tasks and moves them to the active projects and sets the states to the tasks.
        /// </summary>
        /// <param name="itemStates">The item states.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="itemStates" /> is <see langword="null" /></exception>
        public Task UncompleteAsync(params ItemState[] itemStates)
        {
            if (itemStates == null)
            {
                throw new ArgumentNullException(nameof(itemStates));
            }

            var command = new Command(CommandType.UncompleteItem, new UncompleteItemsArgument(itemStates));
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
        /// Updates the multiple orders indents asynchronous.
        /// </summary>
        /// <param name="idsToOrderIndents">The ids to order indents.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="idsToOrderIndents"/> is <see langword="null"/></exception>
        public Task UpdateMultipleOrdersIndentsAsync(params OrderIndentEntry[] idsToOrderIndents)
        {
            if (idsToOrderIndents == null)
            {
                throw new ArgumentNullException(nameof(idsToOrderIndents));
            }

            var command = new Command(
                              CommandType.UpdateOrderIndentsItem,
                              new IdsToOrderIndentsArgument(idsToOrderIndents));
            return ExecuteCommandAsync(command);
        }
    }
}
