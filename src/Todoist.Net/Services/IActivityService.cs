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
        /// Gets list of activity logs.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The activity log entries.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>The activity log is only available for Todoist Premium.</remarks>
        Task<Activity> GetAsync(LogFilter filter = null, CancellationToken cancellationToken = default);
    }
}
