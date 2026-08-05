using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist backups management.
    /// </summary>
    internal class BackupsService : ServiceBase, IBackupsService
    {
        internal BackupsService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<Backup>> GetAsync(string mfaToken = null, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();
            parameters.AddIfNotNullOrEmpty("mfa_token", mfaToken);
            
            return TodoistClient.GetAsync<IReadOnlyCollection<Backup>>("backups", parameters, cancellationToken);
        }
    }
}
