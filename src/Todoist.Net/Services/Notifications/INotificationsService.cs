using System.Collections.Generic;
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
        /// Gets a read-only collection of notifications that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all notifications and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of notifications that were synchronized.
        /// </returns>
        Task<SyncResponse<Notification>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a type-based map of notification settings that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all notification settings and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a type-based map of notification settings that were synchronized.
        /// </returns>
        Task<EntitySyncResponse<Dictionary<NotificationType, NotificationSetting>>> SyncSettingsAsync(
            string syncToken = "*", 
            CancellationToken cancellationToken = default);
    }
}
