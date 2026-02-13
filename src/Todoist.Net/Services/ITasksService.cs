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
        /// Gets all tasks.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The tasks.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<DetailedTask>> GetAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a task by ID.
        /// </summary>
        /// <param name="id">The ID of the task.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The task.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<DetailedTask> GetAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets completed tasks by completion date.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A paginated response containing completed tasks.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>Only available for Todoist Premium users.</remarks>
        Task<PaginatedItemsResponse<CompletedTask>> GetCompletedByCompletionDateAsync(TaskFilter filter = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets completed tasks by due date.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A paginated response containing completed tasks.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>Only available for Todoist Premium users.</remarks>
        Task<PaginatedItemsResponse<CompletedTask>> GetCompletedByDueDateAsync(TaskFilter filter = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add a task. Implementation of the Quick Add Task available in the official clients.
        /// </summary>
        /// <param name="quickAddTask">The quick add task.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The created task.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="quickAddTask"/> is <see langword="null"/></exception>
        Task<DetailedTask> QuickAddAsync(QuickAddTask quickAddTask, CancellationToken cancellationToken = default);
    }
}
