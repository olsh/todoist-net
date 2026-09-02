using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal class NotificationsService : NotificationsCommandService, INotificationsService
    {
        public NotificationsService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        public NotificationsService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <inheritdoc/>
        public Task<SyncResponse<Notification>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncResourceAsync(ResourceType.LiveNotifications, r => r.Notifications, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<EntitySyncResponse<Dictionary<NotificationType, NotificationSetting>>> SyncSettingsAsync(
            string syncToken = "*", 
            CancellationToken cancellationToken = default)
        {
            return SyncEntityResourceAsync(ResourceType.NotificationSettings, r => r.NotificationSettings, syncToken, cancellationToken);
        }
    }
}
