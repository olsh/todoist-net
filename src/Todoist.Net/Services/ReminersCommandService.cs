using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal class ReminersCommandService : CommandServiceBase, IReminersCommandService
    {
        internal ReminersCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal ReminersCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <summary>
        /// Adds a reminder asynchronous.
        /// </summary>
        /// <param name="reminder">The reminder.</param>
        /// <returns>
        /// The reminder ID.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="reminder" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task<ComplexId> AddAsync(Reminder reminder)
        {
            if (reminder == null)
            {
                throw new ArgumentNullException(nameof(reminder));
            }

            var command = CreateAddCommand(CommandType.AddReminder, reminder);
            await ExecuteCommandAsync(command).ConfigureAwait(false);

            return reminder.Id;
        }

        /// <summary>
        /// Update the orders of multiple filters at once.
        /// </summary>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        public async Task ClearLocationsAsync()
        {
            var command = new Command(CommandType.ClearLocations, EmptyCommand.Instance);
            await ExecuteCommandAsync(command).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes an existing reminder asynchronous.
        /// </summary>
        /// <param name="id">The ID of the reminder.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task DeleteAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.DeleteReminder, id);
            await ExecuteCommandAsync(command).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates a reminder asynchronous.
        /// </summary>
        /// <param name="reminder">The reminder.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="reminder"/> is <see langword="null"/></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task UpdateAsync(Reminder reminder)
        {
            if (reminder == null)
            {
                throw new ArgumentNullException(nameof(reminder));
            }

            var command = new Command(CommandType.UpdateReminder, reminder);
            await ExecuteCommandAsync(command).ConfigureAwait(false);
        }
    }
}
