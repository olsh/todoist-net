using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for view options which can be executed in a transaction.
    /// </summary>
    public interface IViewOptionsCommandService
    {
        /// <summary>
        /// Sets view options for a view scope.
        /// </summary>
        /// <param name="viewOptions">The view options payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewOptions" /> is <see langword="null" />.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task SetAsync(ViewOptions viewOptions, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes view options for a view scope.
        /// </summary>
        /// <param name="viewOptions">The view options payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewOptions" /> is <see langword="null" />.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task DeleteAsync(ViewOptions viewOptions, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets project-level default view options.
        /// </summary>
        /// <param name="viewDefaults">The project view defaults payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewDefaults" /> is <see langword="null" />.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task SetProjectDefaultsAsync(ProjectViewOptionsDefaults viewDefaults, CancellationToken cancellationToken = default);
    }
}
