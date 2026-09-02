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
        /// Gets a read-only collection of filters that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all filters and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of filters that were synchronized.
        /// </returns>
        Task<SyncResponse<Filter>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default);
    }
}
