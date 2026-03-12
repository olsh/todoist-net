using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for user management.
    /// </summary>
    public interface IUserService : IUserCommandService
    {
        /// <summary>
        /// Gets the current user info that were synchronized with the specified sync token.
        /// </summary>
        /// <remarks>
        /// When a valid sync token is provided, the API returns the current user info only if it has changed since the last sync. 
        /// If no changes were made since the last sync, the user info value returned in the response will be <see langword="null" />. 
        /// </remarks>
        /// <param name="syncToken">The sync token. Use "*" to get all projects and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the current user info that were synchronized.
        /// </returns>
        Task<EntitySyncResponse<UserInfo>> SyncInfoAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the current user settings that were synchronized with the specified sync token.
        /// </summary>
        /// <remarks>
        /// When a valid sync token is provided, the API returns the current user settings only if it has changed since the last sync. 
        /// If no changes were made since the last sync, the user settings value returned in the response will be <see langword="null" />. 
        /// </remarks>
        /// <param name="syncToken">The sync token. Use "*" to get all project view options defaults and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the current user settings that were synchronized.
        /// </returns>
        Task<EntitySyncResponse<UserSettings>> SyncSettingsAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the current user plan limits that were synchronized with the specified sync token.
        /// </summary>
        /// <remarks>
        /// When a valid sync token is provided, the API returns the current user plan limits only if it has changed since the last sync. 
        /// If no changes were made since the last sync, the user plan limits value returned in the response will be <see langword="null" />. 
        /// </remarks>
        /// <param name="syncToken">The sync token. Use "*" to get all project view options defaults and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the current user plan limits that were synchronized.
        /// </returns>
        Task<EntitySyncResponse<UserPlanLimits>> SyncPlanLimitsAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the current user productivity stats that were synchronized with the specified sync token.
        /// </summary>
        /// <remarks>
        /// When a valid sync token is provided, the API returns the current user productivity stats only if it has changed since the last sync. 
        /// If no changes were made since the last sync, the user productivity stats value returned in the response will be <see langword="null" />. 
        /// </remarks>
        /// <param name="syncToken">The sync token. Use "*" to get all project view options defaults and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the current user productivity stats that were synchronized.
        /// </returns>
        Task<EntitySyncResponse<UserStats>> SyncStatsAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the current user info.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The current user info.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<UserInfo> GetInfoAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the current user productivity stats.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The current user productivity stats.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<DetailedUserStats> GetStatsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a notification setting.
        /// </summary>
        /// <param name="setting">The setting update payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The notification settings by notification type.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="setting"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task UpdateNotificationSettingAsync(NotificationSettingUpdate setting, CancellationToken cancellationToken = default);
    }
}
