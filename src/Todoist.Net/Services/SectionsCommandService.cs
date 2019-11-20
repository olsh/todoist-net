using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for sections management which can be executed in a transaction.
    /// </summary>
    /// <seealso cref="CommandServiceBase" />
    /// <seealso cref="Todoist.Net.Services.ISectionsCommandService" />
    internal class SectionsCommandService : CommandServiceBase, ISectionsCommandService
    {
        internal SectionsCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal SectionsCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <summary>
        /// Add a new section to a project.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <returns>The ID of the section.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="section" /> is <see langword="null" /></exception>
        public async Task<ComplexId> AddAsync(Section section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            var command = CreateAddCommand(CommandType.AddSection, section);
            await ExecuteCommandAsync(command)
                .ConfigureAwait(false);

            return section.Id;
        }

        /// <summary>
        /// Delete a section and all its descendants items.
        /// </summary>
        /// <param name="id">The section ID.</param>
        /// <returns> Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task DeleteAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.DeleteSection, id);

            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Archive a section and all its descendants tasks.
        /// </summary>
        /// <param name="id">The section ID.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task ArchiveAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.ArchiveSection, id);

            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Unarchive a section.
        /// </summary>
        /// <param name="id">The section ID.</param>
        /// <returns> Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task UnarchiveAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.UnarchiveSection, id);

            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Updates the section.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="section" /> is <see langword="null" /></exception>
        public Task UpdateAsync(Section section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            var command = new Command(CommandType.UpdateSection, section);

            return ExecuteCommandAsync(command);
        }
    }
}
