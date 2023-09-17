using System;
using System.Collections.Generic;
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
        public async Task<ComplexId> AddAsync(Label label)
        {
            if (label == null)
            {
                throw new ArgumentNullException(nameof(label));
            }

            var command = CreateAddCommand(CommandType.AddLabel, label);
            await ExecuteCommandAsync(command).ConfigureAwait(false);

            return label.Id;
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.DeleteLabel, id);
            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Label label)
        {
            if (label == null)
            {
                throw new ArgumentNullException(nameof(label));
            }

            var command = new Command(CommandType.UpdateLabel, label);
            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task UpdateOrderAsync(params OrderEntry[] orderEntries)
        {
            if (orderEntries == null)
            {
                throw new ArgumentNullException(nameof(orderEntries));
            }

            var command = new Command(CommandType.UpdateOrderLabel, new IdToOrderMappingArgument(orderEntries));
            return ExecuteCommandAsync(command);
        }
    }
}
