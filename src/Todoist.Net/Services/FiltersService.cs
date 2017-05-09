using System.Collections.Generic;
using System.Net.Http;
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

        /// <summary>
        /// Gets all filters.
        /// </summary>
        /// <returns>The filters.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task<IEnumerable<Filter>> GetAsync()
        {
            var response = await TodoistClient.GetResourcesAsync(ResourceType.Filters).ConfigureAwait(false);

            return response.Filters;
        }

        /// <summary>
        /// Gets a filter info by ID.
        /// </summary>
        /// <param name="id">The ID of the filter.</param>
        /// <returns>
        /// The filter info.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
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
