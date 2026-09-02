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
        public Task<ComplexId> AddAsync(AddSection section, CancellationToken cancellationToken = default)
        {
            return ExecuteAddCommandAsync(CommandType.AddSection, section, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(UpdateSection section, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateSection, section, cancellationToken);
        }

        /// <inheritdoc/>
        public Task MoveAsync(MoveSectionArgument moveArgument, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.MoveSection, moveArgument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task ReorderAsync(ReorderSectionsArgument reorderArgument, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.ReorderSections, reorderArgument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return ExecuteEntityCommandAsync(CommandType.DeleteSection, id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task ArchiveAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return ExecuteEntityCommandAsync(CommandType.ArchiveSection, id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UnarchiveAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return ExecuteEntityCommandAsync(CommandType.UnarchiveSection, id, cancellationToken);
        }
    }
}
