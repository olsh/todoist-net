using System;
using System.Collections.Generic;
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public Task ArchiveAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.ArchiveSection, id);

            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.DeleteSection, id);

            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task MoveAsync(SectionMoveArgument moveArgument)
        {
            if (moveArgument == null)
            {
                throw new ArgumentNullException(nameof(moveArgument));
            }

            var command = new Command(CommandType.MoveSection, moveArgument);

            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task ReorderAsync(params SectionOrderEntry[] orderEntries)
        {
            if (orderEntries == null)
            {
                throw new ArgumentNullException(nameof(orderEntries));
            }

            var command = new Command(CommandType.ReorderSection, new ReorderSectionArgument(orderEntries));

            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task UnarchiveAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.UnarchiveSection, id);

            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
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
