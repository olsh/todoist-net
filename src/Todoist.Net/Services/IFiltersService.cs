using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The filters.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Filter>> GetAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a filter info by ID.
        /// </summary>
        /// <param name="id">The ID of the filter.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The filter info.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<FilterInfo> GetAsync(ComplexId id, CancellationToken cancellationToken = default);
    }
}
