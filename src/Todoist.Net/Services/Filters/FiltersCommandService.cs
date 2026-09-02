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
        public Task<ComplexId> AddAsync(Filter filter, CancellationToken cancellationToken = default)
        {
            return ExecuteAddCommandAsync(CommandType.AddFilter, filter, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Filter filter, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateFilter, filter, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return ExecuteEntityCommandAsync(CommandType.DeleteFilter, id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateOrderAsync(IdToOrderMappingArgument orderMapping, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateFilterOrders, orderMapping, cancellationToken);
        }
    }
}
