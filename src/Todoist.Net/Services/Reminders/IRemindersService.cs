using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for reminders management.
    /// </summary>
    public interface IRemindersService : IRemindersCommandService
    {
        /// <summary>
        /// Gets a read-only collection of reminders that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all reminders and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of reminders that were synchronized.
        /// </returns>
        Task<SyncResponse<Reminder>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a read-only collection of reminder location string arrays that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all reminder locations and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of reminder location string arrays that were synchronized.
        /// </returns>
        Task<SyncResponse<string[]>> SyncLocationsAsync(string syncToken = "*", CancellationToken cancellationToken = default);
    }
}
