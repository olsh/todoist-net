using System;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for filters management.
    /// </summary>
    public interface IFiltersCommandService
    {
        /// <summary>
        /// Adds a filter asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>
        /// The temporary ID of the filter.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="filter" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ComplexId> AddAsync(Filter filter);

        /// <summary>
        /// Deletes an existing filter asynchronous.
        /// </summary>
        /// <param name="id">The ID of the filter.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(ComplexId id);

        /// <summary>
        /// Updates a filter asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="filter"/> is <see langword="null"/></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task UpdateAsync(Filter filter);

        /// <summary>
        /// Update the orders of multiple filters at once.
        /// </summary>
        /// <param name="orderEntries">The order entries.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="orderEntries"/> is <see langword="null"/></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        Task UpdateOrderAsync(params OrderEntry[] orderEntries);
    }
}
