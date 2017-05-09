using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for projects management which can be executed in a transaction.
    /// </summary>
    /// <seealso cref="CommandServiceBase" />
    /// <seealso cref="Todoist.Net.Services.IProjectCommandService" />
    internal class ProjectsCommandService : CommandServiceBase, IProjectCommandService
    {
        internal ProjectsCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal ProjectsCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <summary>
        /// Adds a new project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>The ID of the project.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="project"/> is <see langword="null"/></exception>
        public async Task<ComplexId> AddAsync(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            var command = CreateAddCommand(CommandType.AddProject, project);
            await ExecuteCommandAsync(command).ConfigureAwait(false);

            return project.Id;
        }

        /// <summary>
        /// Archives the project and its children.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>Only available for Todoist Premium users.</remarks>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        public Task ArchiveAsync(params ComplexId[] ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var command = CreateCollectionCommand(CommandType.ArchiveProject, ids);
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Deletes existing projects.
        /// </summary>
        /// <param name="ids">The IDs.</param>
        /// <returns> Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        public Task DeleteAsync(params ComplexId[] ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var command = CreateCollectionCommand(CommandType.DeleteProject, ids);
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Un archive project and its children.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns> Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        /// <remarks>Only available for Todoist Premium users.</remarks>
        public Task UnarchiveAsync(params ComplexId[] ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var command = CreateCollectionCommand(CommandType.UnarchiveProject, ids);
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Updates the project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="project"/> is <see langword="null"/></exception>
        public Task UpdateAsync(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            var command = new Command(CommandType.UpdateProject, project);
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Updates the multiple orders indents asynchronous.
        /// </summary>
        /// <param name="idsToOrderIndents">The ids to order indents.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="idsToOrderIndents"/> is <see langword="null"/></exception>
        public Task UpdateMultipleOrdersIndentsAsync(params OrderIndentEntry[] idsToOrderIndents)
        {
            if (idsToOrderIndents == null)
            {
                throw new ArgumentNullException(nameof(idsToOrderIndents));
            }

            var command = new Command(
                              CommandType.UpdateOrderIndentsProject,
                              new IdsToOrderIndentsArgument(idsToOrderIndents));
            return ExecuteCommandAsync(command);
        }
    }
}
