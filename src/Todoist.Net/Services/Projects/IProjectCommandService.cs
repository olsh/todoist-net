using System;
using System.Net.Http;
using System.Threading;
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
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The temporary ID of the project.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="project"/> is <see langword="null"/></exception>
        Task<ComplexId> AddAsync(AddProject project, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="project"/> is <see langword="null"/></exception>
        Task UpdateAsync(UpdateProject project, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates parent project relationships of the project asynchronous.
        /// </summary>
        /// <param name="moveArgument">The move command argument.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="moveArgument" /> is <see langword="null" /></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task MoveAsync(MoveArgument moveArgument, CancellationToken cancellationToken = default);

        /// <summary>
        /// Moves a project to a workspace asynchronous.
        /// </summary>
        /// <param name="moveArgument">The move command argument.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="moveArgument" /> is <see langword="null" /></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task MoveToWorkspaceAsync(MoveProjectToWorkspaceArgument moveArgument, CancellationToken cancellationToken = default);

        /// <summary>
        /// Moves a project to personal projects asynchronous.
        /// </summary>
        /// <param name="projectId">The project ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task MoveToPersonalAsync(ComplexId projectId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Leaves a workspace project.
        /// </summary>
        /// <param name="projectId">The project ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task LeaveAsync(ComplexId projectId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete an existing project and all its descendants.
        /// </summary>
        /// <param name="id">The project ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns> Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/></exception>
        Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Archive project and its children.
        /// </summary>
        /// <param name="id">The ids.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/></exception>
        /// <remarks>Only available for Todoist Premium users.</remarks>
        Task ArchiveAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Un archive project and its children.
        /// </summary>
        /// <param name="id">The ids.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns> Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/></exception>
        /// <remarks>Only available for Todoist Premium users.</remarks>
        Task UnarchiveAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update the orders and indents of multiple projects at once asynchronous.
        /// </summary>
        /// <param name="reorderArgument">The reorder argument.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reorderArgument" /> is <see langword="null" /></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="T:System.ArgumentException">Value cannot be an empty collection.</exception>
        Task ReorderAsync(ReorderProjectsArgument reorderArgument, CancellationToken cancellationToken = default);

        /// <summary>
        /// Changes the role of a collaborator in a workspace project.
        /// </summary>
        /// <param name="argument">The role change payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="argument" /> is <see langword="null" />.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task ChangeRoleAsync(ChangeProjectRoleArgument argument, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets project-level default view options.
        /// </summary>
        /// <param name="viewDefaults">The project view defaults payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="viewDefaults" /> is <see langword="null" />.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task SetViewOptionsDefaultsAsync(ProjectViewOptionsDefaults viewDefaults, CancellationToken cancellationToken = default);
    }
}
