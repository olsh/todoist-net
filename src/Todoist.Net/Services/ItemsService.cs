using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<DetailedTask>> GetAsync(CancellationToken cancellationToken = default)
        {
            var response = await TodoistClient.GetResourcesAsync(cancellationToken, ResourceType.Tasks).ConfigureAwait(false);

            return response.Tasks;
        }

        /// <inheritdoc/>
        public Task<TaskInfo> GetAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return TodoistClient.PostAsync<TaskInfo>(
                "items/get",
                new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>(
                            "item_id",
                            id.ToString())
                    },
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<CompletedTasksInfo> GetCompletedAsync(TaskFilter filter = null, CancellationToken cancellationToken = default)
        {
            var parameters = filter == null ? new List<KeyValuePair<string, string>>() : filter.ToParameters();

            return TodoistClient.GetAsync<CompletedTasksInfo>("completed/get_all", parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<DetailedTask> QuickAddAsync(QuickAddTask quickAddTask, CancellationToken cancellationToken = default)
        {
            if (quickAddTask == null)
            {
                throw new ArgumentNullException(nameof(quickAddTask));
            }

            return TodoistClient.PostAsync<DetailedTask>("quick/add", quickAddTask.ToParameters(), cancellationToken);
        }
    }
}
