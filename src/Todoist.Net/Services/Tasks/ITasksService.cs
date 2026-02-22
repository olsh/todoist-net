using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for Todoist tasks management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.TasksCommandService" />
    public interface ITasksService : ITasksCommandService
    {
        /// <summary>
        /// Gets a read-only collection of tasks that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all tasks and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of tasks that were synchronized.
        /// </returns>
        Task<SyncResponse<TaskInfo>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets day orders that were synchronized with the specified sync token.
         /// </summary>
         /// <param name="syncToken">The sync token. Use "*" to get all day orders and the new sync token.</param>
         /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
         /// <returns>
         /// A task that represents the asynchronous operation. The task result contains a read-only collection of day orders that were synchronized.
         /// </returns>
        Task<EntitySyncResponse<Dictionary<string, int>>> SyncDayOrdersAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets tasks with cursor/limit pagination.
        /// </summary>
        /// <param name="query">The pagination query.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The tasks.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedResponse<TaskInfo>> GetAsync(TasksPaginationQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets tasks based on a filter query with cursor/limit pagination.
        /// </summary>
        /// <param name="query">The filter query.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The tasks.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedResponse<TaskInfo>> GetByFilterAsync(TasksFilterQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets completed tasks with cursor/limit pagination ordered by completion date.
         /// </summary>
         /// <param name="query">The pagination query.</param>
         /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
         /// <returns>
         /// The completed tasks.
         /// </returns>
         /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedCompletedTasks> GetCompletedByCompletionDateAsync(CompletedTasksPaginationQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets completed tasks with cursor/limit pagination ordered by due date.
         /// </summary>
         /// <param name="query">The pagination query.</param>
         /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
         /// <returns>
         /// The completed tasks.
         /// </returns>
         /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedCompletedTasks> GetCompletedByDueDateAsync(CompletedTasksPaginationQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a task by ID.
        /// </summary>
        /// <param name="id">The ID of the task.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The task.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<TaskInfo> GetAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add a task. Implementation of the Quick Add Task available in the official clients.
        /// </summary>
        /// <param name="quickAddTask">The quick add task.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The created task.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="quickAddTask"/> is <see langword="null"/></exception>
        Task QuickAddAsync(QuickAddTask quickAddTask, CancellationToken cancellationToken = default);
    }
}
