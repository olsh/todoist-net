using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;
using Todoist.Net.Services;

namespace Todoist.Net
{
    /// <summary>
    /// A Todoist client.
    /// </summary>
    public interface ITodoistClient
    {
        /// <summary>Gets the ID mappings helper service.</summary>
        IIdsService Ids { get; }

        /// <summary>Gets the workspace service.</summary>
        IWorkspacesService Workspaces { get; }

        /// <summary>Gets the workspace filters service.</summary>
        IWorkspaceFiltersService WorkspaceFilters { get; }

        /// <summary>Gets the projects service.</summary>
        IProjectsService Projects { get; }

        /// <summary>Gets the comments service.</summary>
        ICommentsService Comments { get; }

        /// <summary>Gets the templates.</summary>
        /// <remarks>Templates are only available for Todoist Premium users.</remarks>
        ITemplatesService Templates { get; }

        /// <summary>Gets the sections service.</summary>
        ISectionsService Sections { get; }

        /// <summary>Gets the tasks service.</summary>
        ITasksService Tasks { get; }

        /// <summary>Gets the labels service.</summary>
        ILabelsService Labels { get; }

        /// <summary>Gets the uploads service.</summary>
        IUploadsService Uploads { get; }

        /// <summary>Gets the filters service.</summary>
        /// <remarks>Filters are only available for Todoist Premium users.</remarks>
        IFiltersService Filters { get; }

        /// <summary>Gets the reminders service.</summary>
        /// <remarks>Reminders are only available for Todoist Premium users.</remarks>
        IRemindersService Reminders { get; }

        /// <summary>Gets the users service.</summary>
        IUsersService Users { get; }

        /// <summary>Gets the activity service.</summary>
        IActivityService Activity { get; }

        /// <summary>Gets the backups service.</summary>
        IBackupsService Backups { get; }

        /// <summary>Gets the email service.</summary>
        /// <remarks>Provides access to email settings and email-specific operations.</remarks>
        IEmailsService Emails { get; }

        /// <summary>Gets the view options service.</summary>
        IViewOptionsService ViewOptions { get; }

        /// <summary>Gets the sharing service.</summary>
        ISharingService Sharing { get; }

        /// <summary>Gets the notifications service.</summary>
        INotificationsService Notifications { get; }

        /// <summary>Gets the calendars service.</summary>
        ICalendarsService Calendars { get; }


        /// <summary>
        /// Creates the transaction.
        /// </summary>
        /// <returns>The transaction.</returns>
        ITransaction CreateTransaction();

        /// <summary>
        /// Gets the synchronized resources asynchronous. Returns all resources if zero or <c>null</c> resource types were passed.
        /// </summary>
        /// <remarks>
        /// When sync token is passed in only tasks that have changed since last Sync will be returned.
        /// </remarks>
        /// <param name="syncToken">The sync token returned from Todoist for increment sync</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <param name="resourceTypes">The resource types.</param>
        /// <returns>
        /// The requested resources.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<SyncResourcesResponse> SyncResourcesAsync(ResourceType[] resourceTypes = null, string syncToken = "*", CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Gets the synchronized resources asynchronous. Returns all resources if zero or <c>null</c> resource types were passed.
        /// </summary>
        /// <remarks>
        /// When sync token is passed in only tasks that have changed since last Sync will be returned.
        /// </remarks>
        /// <param name="syncToken">The sync token returned from Todoist for increment sync</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <param name="resourceTypes">The resource types.</param>
        /// <returns>        
        /// The requested resources.
        /// </returns>
        Task<T> SyncResourcesAsync<T>(ResourceType[] resourceTypes = null, string syncToken = "*", CancellationToken cancellationToken = default)
             where T : BaseSyncResponse;
    }
}
