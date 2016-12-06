using System.Collections.Generic;
using System.Net.Http;
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
        /// <returns>List of the notifications.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Notification>> GetAsync();
    }
}
