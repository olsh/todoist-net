using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Represents a Transaction
    /// </summary>
    public interface ITransaction
    {
        /// <summary>
        /// Gets the items service.
        /// </summary>
        /// <value>The items service.</value>
        IItemsCommandService Items { get; }

        /// <summary>
        /// Gets the labels service.
        /// </summary>
        /// <value>The labels service.</value>
        ILabelsCommandService Labels { get; }

        /// <summary>
        /// Gets the notes service.
        /// </summary>
        /// <value>The notes service.</value>
        INotesCommandServices Notes { get; }

        /// <summary>
        /// Gets the notifications service.
        /// </summary>
        /// <value>The notifications service.</value>
        INotificationsCommandService Notifications { get; }

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>The project.</value>
        IProjectCommandService Project { get; }

        /// <summary>
        /// Commits the transaction asynchronous.
        /// </summary>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task CommitAsync();
    }
}
