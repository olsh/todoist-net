using System;
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
    /// <seealso cref="Todoist.Net.Services.IItemsService" />
    internal class ItemsService : ItemsCommandService, IItemsService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsService"/> class.
        /// </summary>
        /// <param name="todoistClient">The todoist client.</param>
        internal ItemsService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns>The items.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task<IEnumerable<Item>> GetAsync()
        {
            var response = await TodoistClient.GetResourcesAsync(ResourceType.Items).ConfigureAwait(false);

            return response.Items;
        }

        /// <summary>
        /// Gets an item by ID.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <returns>The item.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task<ItemInfo> GetAsync(ComplexId id)
        {
            return TodoistClient.PostAsync<ItemInfo>(
                "items/get",
                new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>(
                            "item_id",
                            id.ToString())
                    });
        }

        /// <summary>
        /// Gets all the user’s completed items (tasks).
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The completed items.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <remarks>Only available for Todoist Premium users.</remarks>
        public Task<CompletedItemsInfo> GetCompletedAsync(ItemFilter filter = null)
        {
            var parameters = filter == null ? new List<KeyValuePair<string, string>>() : filter.ToParameters();

            return TodoistClient.PostAsync<CompletedItemsInfo>("completed/get_all", parameters);
        }

        /// <summary>
        /// Add a task. Implementation of the Quick Add Task available in the official clients.
        /// </summary>
        /// <param name="quickAddItem">The quick add item.</param>
        /// <returns>The created task.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="quickAddItem"/> is <see langword="null"/></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task<Item> QuickAddAsync(QuickAddItem quickAddItem)
        {
            if (quickAddItem == null)
            {
                throw new ArgumentNullException(nameof(quickAddItem));
            }

            return TodoistClient.PostAsync<Item>("quick/add", quickAddItem.ToParameters());
        }
    }
}
