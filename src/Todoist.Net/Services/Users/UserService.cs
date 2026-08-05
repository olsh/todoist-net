using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for user management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.UserCommandService" />
    /// <seealso cref="Todoist.Net.Services.IUserService" />
    internal class UserService : UserCommandService, IUserService
    {
        internal UserService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<EntitySyncResponse<UserInfo>> SyncInfoAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncEntityResourceAsync(ResourceType.User, r => r.UserInfo, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<EntitySyncResponse<UserSettings>> SyncSettingsAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncEntityResourceAsync(ResourceType.UserSettings, r => r.UserSettings, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<EntitySyncResponse<UserPlanLimits>> SyncPlanLimitsAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncEntityResourceAsync(ResourceType.UserPlanLimits, r => r.UserPlanLimits, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<EntitySyncResponse<UserStats>> SyncStatsAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncEntityResourceAsync(ResourceType.UserStats, r => r.UserStats, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<UserInfo> GetInfoAsync(CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<UserInfo>("user", cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public Task<DetailedUserStats> GetStatsAsync(CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<DetailedUserStats>("tasks/completed/stats", cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateNotificationSettingAsync(NotificationSettingUpdate setting, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNull(setting, nameof(setting));

            return TodoistClient.PutJsonAsync<NotificationSettingUpdate>("notification_setting", setting, cancellationToken);
        }
    }
}
