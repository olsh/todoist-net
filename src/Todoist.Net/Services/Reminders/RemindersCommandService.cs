using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal class RemindersCommandService : CommandServiceBase, IRemindersCommandService
    {
        internal RemindersCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal RemindersCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <inheritdoc/>
        public Task<ComplexId> AddAsync(AddReminder reminder, CancellationToken cancellationToken = default)
        {
            return ExecuteAddCommandAsync(CommandType.AddReminder, reminder, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Reminder reminder, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateReminder, reminder, cancellationToken);
        }
        
        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return ExecuteEntityCommandAsync(CommandType.DeleteReminder, id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task ClearLocationsAsync(CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.ClearLocations, cancellationToken);
        }
    }
}
