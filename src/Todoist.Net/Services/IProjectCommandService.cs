using System;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;
using Todoist.Net.Models.Types;

namespace Todoist.Net.Services
{
    public interface IProjectCommandService
    {
        /// <summary>
        /// Adds a new project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>The task.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="project"/> is <see langword="null"/></exception>
        Task<ComplexId> AddAsync(Project project);

        /// <summary>
        /// Archive project and its children. Only available for Premium users.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>
        /// The task.
        /// </returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        Task ArchiveAsync(params ComplexId[] ids);

        /// <summary>
        /// Deletes existing projects.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>
        /// The task.
        /// </returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        Task DeleteAsync(params ComplexId[] ids);

        /// <summary>
        /// Un archive project and its children. Only available for Premium users.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>
        /// The task.
        /// </returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        Task UnarchiveAsync(params ComplexId[] ids);

        /// <summary>
        /// Updates an existing project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>The task.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="project"/> is <see langword="null"/></exception>
        Task UpdateAsync(Project project);
    }
}