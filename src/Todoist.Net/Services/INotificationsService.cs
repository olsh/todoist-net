using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist notification management.
    /// </summary>
    public interface INotificationsService : INotificationsCommandService
    {
        /// <summary>
        /// Gets all live notifications.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>List of the notifications.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Notification>> GetAsync(CancellationToken cancellationToken = default);
    }
}
