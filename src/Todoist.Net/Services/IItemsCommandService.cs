using System;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for Todoist tasks management which can be executed in a transaction.
    /// </summary>
    public interface IItemsCommandService
    {
        /// <summary>
        /// Adds a new task to a project asynchronous. By default the task is added to the user’s Inbox project.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The temporary ID of the item.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="item" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ComplexId> AddAsync(Item item);

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
        Task CloseAsync(ComplexId id);

        /// <summary>
        /// Completes tasks and optionally move them to history. See also <see cref="ItemsCommandService.CloseAsync" /> for a simplified version of the command.
        /// </summary>
        /// <param name="forceHistory">Whether these tasks should be moved to history.</param>
        /// <param name="ids">The ids of the tasks.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task CompleteAsync(bool forceHistory = true, params ComplexId[] ids);

        /// <summary>
        /// Completes a recurring task. See also <see cref="ItemsCommandService.CloseAsync" /> for a simplified version of the command.
        /// </summary>
        /// <param name="id">The recurring task ID.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task CompleteRecurringAsync(ComplexId id);

        /// <summary>
        /// Completes a recurring task. See also <see cref="ItemsCommandService.CloseAsync" /> for a simplified version of the command.
        /// </summary>
        /// <param name="recurringItemState">State of the recurring item.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="recurringItemState"/> is <see langword="null"/></exception>
        Task CompleteRecurringAsync(RecurringItemState recurringItemState);

        /// <summary>
        /// Deletes an existing task asynchronous.
        /// </summary>
        /// <param name="ids">List of the IDs of the tasks to delete.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(params ComplexId[] ids);

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
        Task MoveToProjectAsync(ComplexId projectId, params Item[] items);

        /// <summary>
        /// Uncompletes tasks and moves them to the active projects.
        /// </summary>
        /// <param name="ids">The ids of the tasks.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task UncompleteAsync(params ComplexId[] ids);

        /// <summary>
        /// Uncompletes tasks and moves them to the active projects and sets the states to the tasks.
        /// </summary>
        /// <param name="itemStates">The item states.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="itemStates" /> is <see langword="null" /></exception>
        Task UncompleteAsync(params ItemState[] itemStates);

        /// <summary>
        /// Updates a task asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task UpdateAsync(Item item);

        /// <summary>
        /// Updates the day orders of multiple items at once.
        /// </summary>
        /// <param name="idsToOrder">The ids to order.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="idsToOrder"/> is <see langword="null"/></exception>
        Task UpdateDayOrdersAsync(params OrderEntry[] idsToOrder);

        /// <summary>
        /// Updates the multiple orders indents asynchronous.
        /// </summary>
        /// <param name="idsToOrderIndents">The ids to order indents.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="idsToOrderIndents"/> is <see langword="null"/></exception>
        Task UpdateMultipleOrdersIndentsAsync(params OrderIndentEntry[] idsToOrderIndents);
    }
}
