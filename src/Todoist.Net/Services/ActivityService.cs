using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist log management.
    /// </summary>
    internal class ActivityService : IActivityService
    {
        private readonly IAdvancedTodoistClient _todoistClient;

        internal ActivityService(IAdvancedTodoistClient todoistClient)
        {
            _todoistClient = todoistClient;
        }

        /// <inheritdoc/>
        public Task<Activity> GetAsync(LogFilter filter = null, CancellationToken cancellationToken = default)
        {
            var parameters = filter != null ? filter.ToParameters() : new List<KeyValuePair<string, string>>();

            return _todoistClient.GetAsync<Activity>("activity/get", parameters, cancellationToken);
        }
    }
}
