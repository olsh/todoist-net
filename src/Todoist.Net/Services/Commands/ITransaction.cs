using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Represents a Transaction
    /// </summary>
    public interface ITransaction
    {
        /// <summary>Gets the workspaces commands service.</summary>
        IWorkspacesCommandService Workspaces { get; }

        /// <summary>Gets the workspace filters commands service.</summary>
        IWorkspaceFiltersCommandService WorkspaceFilters { get; }

        /// <summary>Gets the projects commands service.</summary>
        IProjectsCommandService Projects { get; }

        /// <summary>Gets the comments commands service.</summary>
        ICommentsCommandService Comments { get; }

        /// <summary>Gets the sections commands service.</summary>
        ISectionsCommandService Sections { get; }

        /// <summary>Gets the tasks commands service.</summary>
        ITasksCommandService Tasks { get; }

        /// <summary>Gets the labels commands service.</summary>
        ILabelsCommandService Labels { get; }

        /// <summary>Gets the filters commands service.</summary>
        /// <remarks>Filters are only available for Todoist Premium users.</remarks>
        IFiltersCommandService Filters { get; }

        /// <summary>Gets the reminders commands service.</summary>
        /// <remarks>Reminders are only available for Todoist Premium users.</remarks>
        IRemindersCommandService Reminders { get; }

        /// <summary>Gets the users commands service.</summary>
        IUsersCommandService Users { get; }

        /// <summary>Gets the view options commands service.</summary>
        IViewOptionsCommandService ViewOptions { get; }

        /// <summary>Gets the sharing commands service.</summary>
        ISharingCommandService Sharing { get; }

        /// <summary>Gets the notifications commands service.</summary>
        INotificationsCommandService Notifications { get; }

        /// <summary>
        /// Commits the transaction asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <remarks>
        /// <para>
        /// Any <c>ComplexId</c> values in the commands will be resolved to actual IDs after successful execution using the
        /// <see cref="SyncTransactionResponse.TempIdMappings"/> dictionary in the response, and any command errors will be included in the <see cref="SyncTransactionResponse"/> result for each command.
        /// </para>
        /// </remarks>
        /// <returns>
        /// Returns <see cref="Task{TResult}" />. The task object representing the asynchronous operation 
        /// that at completion returns the transaction response.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<SyncTransactionResponse> CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Commits the transaction asynchronously and synchronizes the specified resource types.
        /// </summary>
        /// <param name="resourceTypes">The resource types to synchronize.</param>
        /// <param name="syncToken">The synchronization token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <remarks>
        /// <para>
        /// Any <c>ComplexId</c> values in the commands will be resolved to actual IDs after successful execution using the
        /// <see cref="SyncTransactionResponse.TempIdMappings"/> dictionary in the response, and any command errors will be included in the <see cref="SyncTransactionResponse"/> result for each command.
        /// </para>
        /// </remarks>
        /// <returns>
        /// Returns <see cref="Task{TResult}" />. The task object representing the asynchronous operation 
        /// that at completion returns the transaction response.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<SyncTransactionResponse> CommitAndSyncAsync(ResourceType[] resourceTypes, string syncToken = "*", CancellationToken cancellationToken = default);
    }
}
