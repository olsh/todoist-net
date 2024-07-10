using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist backups management.
    /// </summary>
    internal class BackupService : IBackupService
    {
        private readonly IAdvancedTodoistClient _todoistClient;

        internal BackupService(IAdvancedTodoistClient todoistClient)
        {
            _todoistClient = todoistClient;
        }

        /// <inheritdoc/>
        public Task<IEnumerable<Backup>> GetAsync(CancellationToken cancellationToken = default)
        {
            return _todoistClient.GetAsync<IEnumerable<Backup>>(
                "backups/get",
                new List<KeyValuePair<string, string>>(),
                cancellationToken);
        }
    }
}
