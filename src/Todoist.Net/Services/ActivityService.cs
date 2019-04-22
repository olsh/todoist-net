using System.Collections.Generic;
using System.Net.Http;
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

        /// <summary>
        /// Gets list of activity logs.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The activity log entries.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>The activity log is only available for Todoist Premium.</remarks>
        public Task<Activity> GetAsync(LogFilter filter = null)
        {
            var parameters = filter != null ? filter.ToParameters() : new List<KeyValuePair<string, string>>();

            return _todoistClient.PostAsync<Activity>("activity/get", parameters);
        }
    }
}
