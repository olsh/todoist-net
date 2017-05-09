using System;
using System.Collections.Generic;
using System.Net.Http;
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

        /// <summary>
        /// Adds a filter asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>
        /// The filter ID.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="filter" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
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

        /// <summary>
        /// Deletes an existing filter asynchronous.
        /// </summary>
        /// <param name="id">The ID of the filter.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task DeleteAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.DeleteFilter, id);
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Updates a filter asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="filter"/> is <see langword="null"/></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task UpdateAsync(Filter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var command = new Command(CommandType.UpdateFilter, filter);
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Update the orders of multiple filters at once.
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

            var command = new Command(CommandType.UpdateOrderFilter, new IdToOrderMappingArgument(orderEntries));
            return ExecuteCommandAsync(command);
        }
    }
}
