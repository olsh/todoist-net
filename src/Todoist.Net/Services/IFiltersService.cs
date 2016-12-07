using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for filters management.
    /// </summary>
    /// <remarks>Filters are only available for Todoist Premium users.</remarks>
    public interface IFiltersService : IFiltersCommandService
    {
        /// <summary>
        /// Gets all filters.
        /// </summary>
        /// <returns>The filters.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Filter>> GetAsync();

        /// <summary>
        /// Gets a filter info by ID.
        /// </summary>
        /// <param name="id">The ID of the filter.</param>
        /// <returns>
        /// The filter info.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<FilterInfo> GetAsync(ComplexId id);
    }
}