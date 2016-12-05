using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;
using Todoist.Net.Models.Types;

namespace Todoist.Net.Services
{
    public sealed class ProjectService : ServiceBase
    {
        internal ProjectService(ITodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal ProjectService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <summary>
        /// Adds a new project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>The task.</returns>
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
        /// Archive project and its children. Only available for Premium users.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>
        /// The task.
        /// </returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        public async Task ArchiveAsync(params ComplexId[] ids)
        {
            var command = CreateCollectionCommand(ids, CommandType.ArchiveProject);
            await ExecuteCommandAsync(command).ConfigureAwait(false);
        }

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
        public async Task DeleteAsync(params ComplexId[] ids)
        {
            var command = CreateCollectionCommand(ids, CommandType.DeleteProject);
            await ExecuteCommandAsync(command).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <returns>The projects.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task<IEnumerable<Project>> GetAsync()
        {
            var response = await TodoistClient.GetResourcesAsync(ResourceType.Projects).ConfigureAwait(false);

            return response.Projects;
        }

        /// <summary>
        /// Gets project by ID.
        /// </summary>
        /// <param name="id">The ID of the project.</param>
        /// <returns>
        /// The project.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task<ProjectInfo> GetAsync(ComplexId id)
        {
            return
                await
                    TodoistClient.GetAsync<ProjectInfo>(
                        "projects/get",
                        new List<KeyValuePair<string, string>>
                            {
                                new KeyValuePair<string, string>("project_id", id.ToString())
                            }).ConfigureAwait(false);
        }

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
        public async Task UnarchiveAsync(params ComplexId[] ids)
        {
            var command = CreateCollectionCommand(ids, CommandType.ArchiveProject);
            await ExecuteCommandAsync(command).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates an existing project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>The task.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="project"/> is <see langword="null"/></exception>
        public async Task UpdateAsync(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            var command = new Command(CommandType.UpdateProject, project, null);
            await ExecuteCommandAsync(command).ConfigureAwait(false);
        }
    }
}
