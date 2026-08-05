using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist calendar management.
    /// </summary>
    public interface ICalendarsService
    {
        /// <summary>
        /// Gets a read-only collection of calendars that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all calendars and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of calendars that were synchronized.
        /// </returns>
        Task<SyncResponse<Calendar>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a read-only collection of calendar accounts that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all calendar accounts and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of calendar accounts that were synchronized.
        /// </returns>
        Task<SyncResponse<CalendarAccount>> SyncAccountsAsync(string syncToken = "*", CancellationToken cancellationToken = default);
    }
}
