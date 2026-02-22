using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist log management.
    /// </summary>
    public interface IActivityService
    {
        /// <summary>
        /// Gets a paginated list of activity logs.
        /// </summary>
        /// <param name="query">The query parameters for filtering and pagination.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A paginated response containing activity log entries and cursor data for continuation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>The activity log is only available for Todoist Premium.</remarks>
        Task<PaginatedResponse<ActivityLog>> GetAsync(LogsPaginationQuery query = null, CancellationToken cancellationToken = default);
    }
}
