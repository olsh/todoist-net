using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for Todoist tasks management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.TasksCommandService" />
    /// <seealso cref="Todoist.Net.Services.ITasksService" />
    internal class TasksService : TasksCommandService, ITasksService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TasksService"/> class.
        /// </summary>
        /// <param name="todoistClient">The todoist client.</param>
        internal TasksService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<SyncResponse<TaskInfo>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncResourceAsync(ResourceType.Tasks, r => r.Tasks, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<EntitySyncResponse<Dictionary<string, int>>> SyncDayOrdersAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncEntityResourceAsync(ResourceType.DayOrders, r => r.DayOrders, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<TaskInfo>> GetByFilterAsync(TasksFilterQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedResponse<TaskInfo>>("tasks/filter", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedCompletedTasks> GetCompletedByCompletionDateAsync(CompletedTasksPaginationQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedCompletedTasks>("tasks/completed/by_completion_date", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedCompletedTasks> GetCompletedByDueDateAsync(CompletedTasksPaginationQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedCompletedTasks>("tasks/completed/by_due_date", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<TaskInfo>> GetAsync(TasksPaginationQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedResponse<TaskInfo>>("tasks", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<TaskInfo> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(id, nameof(id));

            return TodoistClient.GetAsync<TaskInfo>($"tasks/{id}", cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public Task QuickAddAsync(QuickAddTask quickAddTask, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNull(quickAddTask, nameof(quickAddTask));

            return TodoistClient.PostJsonAsync("tasks/quick", quickAddTask, cancellationToken);
        }
    }
}
