using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for calendars management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.ICalendarsService" />
    internal class CalendarsService : ServiceBase, ICalendarsService
    {
        internal CalendarsService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<SyncResponse<Calendar>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncResourceAsync(ResourceType.Calendars, r => r.Calendars, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<SyncResponse<CalendarAccount>> SyncAccountsAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncResourceAsync(ResourceType.CalendarAccounts, r => r.CalendarAccounts, syncToken, cancellationToken);
        }
    }
}
