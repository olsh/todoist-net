using System;
using System.Net.Http;
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
        /// <summary>
        /// Gets the activity.
        /// </summary>
        /// <value>The activity.</value>
        IActivityService Activity { get; }

        /// <summary>
        /// Gets the backups.
        /// </summary>
        /// <value>The backups.</value>
        IBackupService Backups { get; }

        /// <summary>
        /// Gets the filters.
        /// </summary>
        /// <value>The filters.</value>
        /// <remarks>Filters are only available for Todoist Premium users.</remarks>
        IFiltersService Filters { get; }

        /// <summary>
        /// Gets the items service.
        /// </summary>
        /// <value>
        /// The items service.
        /// </value>
        IItemsService Items { get; }

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <value>The labels.</value>
        ILabelsService Labels { get; }

        /// <summary>
        /// Gets the notes service.
        /// </summary>
        /// <value>
        /// The notes service.
        /// </value>
        INotesServices Notes { get; }

        /// <summary>
        /// Gets the notifications service.
        /// </summary>
        /// <value>The notifications service.</value>
        INotificationsService Notifications { get; }

        /// <summary>
        /// Gets the projects service.
        /// </summary>
        /// <value>
        /// The projects service.
        /// </value>
        IProjectsService Projects { get; }

        /// <summary>
        /// Gets the reminders.
        /// </summary>
        /// <value>The reminders.</value>
        /// <remarks>Reminders are only available for Todoist Premium users.</remarks>
        IRemindersService Reminders { get; }

        /// <summary>
        /// Gets the sharing.
        /// </summary>
        /// <value>
        /// The sharing.
        /// </value>
        ISharingService Sharing { get; }

        /// <summary>
        /// Gets the templates.
        /// </summary>
        /// <value>The templates.</value>
        /// <remarks>Templates are only available for Todoist Premium users.</remarks>
        ITemplateService Templates { get; }

        /// <summary>
        /// Gets the uploads service.
        /// </summary>
        /// <value>
        /// The uploads service.
        /// </value>
        IUploadService Uploads { get; }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>The users.</value>
        IUsersService Users { get; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        /// <remarks>Filters are only available for Todoist Premium users.</remarks>
        IEmailService Emails { get; }

        /// <summary>
        /// Gets the sections service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        ISectionService Sections { get; }

        /// <summary>
        /// Creates the transaction.
        /// </summary>
        /// <returns>The transaction.</returns>
        ITransaction CreateTransaction();

        /// <summary>
        /// Gets the resources asynchronous. Returns all resources if zero resource type were passed.
        /// </summary>
        /// <param name="resourceTypes">The resource types.</param>
        /// <returns>
        /// The requested resources.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="resourceTypes"/> is <see langword="null"/></exception>
        Task<Resources> GetResourcesAsync(params ResourceType[] resourceTypes);
    }
}
