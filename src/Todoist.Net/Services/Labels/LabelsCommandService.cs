using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for labels management which can be executes in a transaction.
    /// </summary>
    /// <seealso cref="CommandServiceBase" />
    /// <seealso cref="Todoist.Net.Services.ILabelsCommandService" />
    internal class LabelsCommandService : CommandServiceBase, ILabelsCommandService
    {
        internal LabelsCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal LabelsCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <inheritdoc/>
        public Task<ComplexId> AddAsync(Label label, CancellationToken cancellationToken = default)
        {
            return ExecuteAddCommandAsync(CommandType.AddLabel, label, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Label label, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateLabel, label, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id, bool keepAsShared = false, CancellationToken cancellationToken = default)
        {
            var argument = new DeleteLabelArgument(id, keepAsShared);
            return ExecuteCommandAsync(CommandType.DeleteLabel, argument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteSharedAsync(string name, CancellationToken cancellationToken = default)
        {
            var argument = new DeleteSharedLabelArgument(name);
            return ExecuteCommandAsync(CommandType.DeleteSharedLabel, argument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task RenameSharedAsync(string name, string newName, CancellationToken cancellationToken = default)
        {
            var argument = new RenameSharedLabelArgument(name, newName);
            return ExecuteCommandAsync(CommandType.RenameSharedLabel, argument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateOrderAsync(IdToOrderMappingArgument orderMapping, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateLabelOrders, orderMapping, cancellationToken);
        }
    }
}
