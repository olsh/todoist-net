using System;
using System.Collections.Generic;
using System.Threading;
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
        public async Task<ComplexId> AddAsync(Section section, CancellationToken cancellationToken = default)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            var command = CreateAddCommand(CommandType.AddSection, section);
            await ExecuteCommandAsync(command, cancellationToken)
                .ConfigureAwait(false);

            return section.Id;
        }

        /// <inheritdoc/>
        public Task ArchiveAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.ArchiveSection, id);

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.DeleteSection, id);

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task MoveAsync(SectionMoveArgument moveArgument, CancellationToken cancellationToken = default)
        {
            if (moveArgument == null)
            {
                throw new ArgumentNullException(nameof(moveArgument));
            }

            var command = new Command(CommandType.MoveSection, moveArgument);

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task ReorderAsync(params SectionOrderEntry[] orderEntries) => ReorderAsync(CancellationToken.None, orderEntries);

        /// <inheritdoc/>
        public Task ReorderAsync(CancellationToken cancellationToken, params SectionOrderEntry[] orderEntries)
        {
            if (orderEntries == null)
            {
                throw new ArgumentNullException(nameof(orderEntries));
            }

            var command = new Command(CommandType.ReorderSection, new ReorderSectionArgument(orderEntries));

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UnarchiveAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.UnarchiveSection, id);

            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Section section, CancellationToken cancellationToken = default)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            var command = new Command(CommandType.UpdateSection, section);

            return ExecuteCommandAsync(command, cancellationToken);
        }
    }
}
