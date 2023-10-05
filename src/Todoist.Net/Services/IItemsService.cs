using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The items.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Item>> GetAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets an item by ID.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The item.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ItemInfo> GetAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all the user’s completed items (tasks).
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The completed items.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>Only available for Todoist Premium users.</remarks>
        Task<CompletedItemsInfo> GetCompletedAsync(ItemFilter filter = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add a task. Implementation of the Quick Add Task available in the official clients.
        /// </summary>
        /// <param name="quickAddItem">The quick add item.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The created task.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="quickAddItem"/> is <see langword="null"/></exception>
        Task<Item> QuickAddAsync(QuickAddItem quickAddItem, CancellationToken cancellationToken = default);
    }
}
