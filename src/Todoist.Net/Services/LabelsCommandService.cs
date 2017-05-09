using System;
using System.Collections.Generic;
using System.Net.Http;
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

        /// <summary>
        /// Adds a label asynchronous.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>
        /// The label ID.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="label" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
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

        /// <summary>
        /// Deletes an existing label asynchronous.
        /// </summary>
        /// <param name="id">The ID of the label.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task DeleteAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.DeleteLabel, id);
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Updates a label asynchronous.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="label"/> is <see langword="null"/></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task UpdateAsync(Label label)
        {
            if (label == null)
            {
                throw new ArgumentNullException(nameof(label));
            }

            var command = new Command(CommandType.UpdateLabel, label);
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Update the orders of multiple labels at once.
        /// </summary>
        /// <param name="orderEntries">The order entries.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="orderEntries"/> is <see langword="null"/></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
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
