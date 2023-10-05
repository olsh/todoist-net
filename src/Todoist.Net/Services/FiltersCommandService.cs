using System;
using System.Collections.Generic;
using System.Threading;
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
        public async Task<ComplexId> AddAsync(Filter filter, CancellationToken cancellationToken = default)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var command = CreateAddCommand(CommandType.AddFilter, filter);
            await ExecuteCommandAsync(command, cancellationToken).ConfigureAwait(false);

            return filter.Id;
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.DeleteFilter, id);
            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Filter filter, CancellationToken cancellationToken = default)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var command = new Command(CommandType.UpdateFilter, filter);
            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateOrderAsync(params OrderEntry[] orderEntries) => UpdateOrderAsync(CancellationToken.None, orderEntries);

        /// <inheritdoc/>
        public Task UpdateOrderAsync(CancellationToken cancellationToken, params OrderEntry[] orderEntries)
        {
            if (orderEntries == null)
            {
                throw new ArgumentNullException(nameof(orderEntries));
            }

            var command = new Command(CommandType.UpdateOrderFilter, new IdToOrderMappingArgument(orderEntries));
            return ExecuteCommandAsync(command, cancellationToken);
        }
    }
}
