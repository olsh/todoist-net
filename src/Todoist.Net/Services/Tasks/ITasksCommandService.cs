using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for Todoist tasks management which can be executed in a transaction.
    /// </summary>
    public interface ITasksCommandService
    {
        /// <summary>
        /// Adds a new task to a project asynchronous. By default the task is added to the user's Inbox project.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The temporary ID of the task.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="task" /> is <see langword="null" /></exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ComplexId> AddAsync(AddTask task, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a task asynchronous.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="task"/> is <see langword="null"/></exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task UpdateAsync(UpdateTask task, CancellationToken cancellationToken = default);

        /// <summary>
        /// Moves task to a different location asynchronous.
        /// </summary>
        /// <param name="moveArgument">The move entry.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="moveArgument" /> is <see langword="null" /></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task MoveAsync(MoveTaskArgument moveArgument, CancellationToken cancellationToken = default);

        /// <summary>
        /// Reorders the tasks.
        /// </summary>
        /// <param name="reorderArgument">The reorder argument.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reorderArgument" /> is <see langword="null" /></exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task ReorderAsync(ReorderTasksArgument reorderArgument, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an existing task asynchronous.
        /// </summary>
        /// <param name="id">List of the IDs of the tasks to delete.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Closes a task asynchronous.
        /// </summary>
        /// <param name="id">The task ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>
        /// A simplified version of task_complete / task_update_date_complete.
        /// The command does exactly what official clients do when you close a task: regular task is completed and moved to history,
        /// subtask is checked (marked as done, but not moved to history), recurring task is moved forward (due date is updated).
        /// </remarks>
        Task CloseAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Completes a recurring task. See also <see cref="CloseAsync" /> for a simplified version of the command.
        /// </summary>
        /// <param name="completeArgument">The complete recurring task argument.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="completeArgument"/> is <see langword="null"/></exception>
        Task CompleteRecurringAsync(CompleteRecurringTaskArgument completeArgument, CancellationToken cancellationToken = default);

        /// <summary>
        /// Completes tasks and optionally move them to history. See also <see cref="CloseAsync" /> for a simplified version of the command.
        /// </summary>
        /// <param name="completeArgument">The complete task argument.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="completeArgument"/> is <see langword="null"/></exception>
        Task CompleteAsync(CompleteTaskArgument completeArgument, CancellationToken cancellationToken = default);

        /// <summary>
        /// Uncompletes tasks and moves them to the active projects.
        /// </summary>
        /// <param name="id">The ids of the tasks.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task UncompleteAsync(ComplexId id, CancellationToken cancellationToken = default);
    }
}
