using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist backups management.
    /// </summary>
    public interface IBackupService
    {
        /// <summary>
        /// Gets list of recent backup archives asynchronous.
        /// </summary>
        /// <returns>The backups information.</returns>
        /// <remarks>Todoist creates a backup archive of users' data on a daily basis. Backup archives can also be accessed from the web app (Todoist Settings -> Backups).</remarks>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Backup>> GetAsync();
    }
}