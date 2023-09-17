using System;
using System.Collections.Generic;
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

        /// <inheritdoc/>
        public async Task<IEnumerable<Item>> GetAsync()
        {
            var response = await TodoistClient.GetResourcesAsync(ResourceType.Items).ConfigureAwait(false);

            return response.Items;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public Task<CompletedItemsInfo> GetCompletedAsync(ItemFilter filter = null)
        {
            var parameters = filter == null ? new List<KeyValuePair<string, string>>() : filter.ToParameters();

            return TodoistClient.PostAsync<CompletedItemsInfo>("completed/get_all", parameters);
        }

        /// <inheritdoc/>
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
