using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for Todoist tasks management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.ItemsCommandService" />
    public interface IItemsService : IItemsCommandService
    {
        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns>The items.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Item>> GetAsync();

        /// <summary>
        /// Gets an item by ID.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <returns>
        /// The item.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ItemInfo> GetAsync(ComplexId id);

        /// <summary>
        /// Gets all the user’s completed items (tasks).
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>
        /// The completed items.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>Only available for Todoist Premium users.</remarks>
        Task<CompletedItemsInfo> GetCompletedAsync(ItemFilter filter = null);
    }
}
