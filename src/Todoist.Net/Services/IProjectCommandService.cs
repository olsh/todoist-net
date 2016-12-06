using System;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for projects management which can be executed in a transaction.
    /// </summary>
    public interface IProjectCommandService
    {
        /// <summary>
        /// Adds a new project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>The ID of the project.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="project"/> is <see langword="null"/></exception>
        Task<ComplexId> AddAsync(Project project);

        /// <summary>
        /// Archive project and its children.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        /// <remarks>Only available for Todoist Premium users.</remarks>
        Task ArchiveAsync(params ComplexId[] ids);

        /// <summary>
        /// Deletes existing projects.
        /// </summary>
        /// <param name="ids">The IDs of the projects.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        Task DeleteAsync(params ComplexId[] ids);

        /// <summary>
        /// Un archive project and its children.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns> Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        /// <remarks>Only available for Todoist Premium users.</remarks>
        Task UnarchiveAsync(params ComplexId[] ids);

        /// <summary>
        /// Updates an existing project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="project"/> is <see langword="null"/></exception>
        Task UpdateAsync(Project project);
    }
}
