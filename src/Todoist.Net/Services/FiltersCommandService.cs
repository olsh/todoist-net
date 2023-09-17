using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal class FiltersCommandService : CommandServiceBase, IFiltersCommandService
    {
        internal FiltersCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal FiltersCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <inheritdoc/>
        public async Task<ComplexId> AddAsync(Filter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var command = CreateAddCommand(CommandType.AddFilter, filter);
            await ExecuteCommandAsync(command).ConfigureAwait(false);

            return filter.Id;
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.DeleteFilter, id);
            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Filter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var command = new Command(CommandType.UpdateFilter, filter);
            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task UpdateOrderAsync(params OrderEntry[] orderEntries)
        {
            if (orderEntries == null)
            {
                throw new ArgumentNullException(nameof(orderEntries));
            }

            var command = new Command(CommandType.UpdateOrderFilter, new IdToOrderMappingArgument(orderEntries));
            return ExecuteCommandAsync(command);
        }
    }
}
