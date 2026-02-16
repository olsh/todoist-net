using System;
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
        public async Task<ComplexId> AddAsync(Reminder reminder, CancellationToken cancellationToken = default)
        {
            if (reminder == null)
            {
                throw new ArgumentNullException(nameof(reminder));
            }

            var command = CreateAddCommand(CommandType.AddReminder, reminder);
            await ExecuteCommandAsync(command, cancellationToken).ConfigureAwait(false);

            return reminder.Id;
        }

        /// <inheritdoc/>
        public Task ClearLocationsAsync(CancellationToken cancellationToken = default)
        {
            var command = new Command(CommandType.ClearLocations, EmptyCommand.Instance);
            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.DeleteReminder, id);
            return ExecuteCommandAsync(command,cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Reminder reminder, CancellationToken cancellationToken = default)
        {
            if (reminder == null)
            {
                throw new ArgumentNullException(nameof(reminder));
            }

            var command = new Command(CommandType.UpdateReminder, reminder);
            return ExecuteCommandAsync(command, cancellationToken);
        }
    }
}
