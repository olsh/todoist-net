using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist log management.
    /// </summary>
    internal class ActivityService : ServiceBase, IActivityService
    {
        internal ActivityService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<ActivityLog>> GetAsync(LogsPaginationQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedResponse<ActivityLog>>("activities", query?.ToParameters(), cancellationToken);
        }
    }
}
