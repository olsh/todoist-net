using System.Collections.Generic;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for filters management.
    /// </summary>
    internal class FiltersService : FiltersCommandService, IFiltersService
    {
        internal FiltersService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Filter>> GetAsync()
        {
            var response = await TodoistClient.GetResourcesAsync(ResourceType.Filters).ConfigureAwait(false);

            return response.Filters;
        }

        /// <inheritdoc/>
        public Task<FilterInfo> GetAsync(ComplexId id)
        {
            return TodoistClient.PostAsync<FilterInfo>(
                "filters/get",
                new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("filter_id", id.ToString())
                    });
        }
    }
}
