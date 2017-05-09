using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for labels management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.LabelsCommandService" />
    /// <seealso cref="Todoist.Net.Services.ILabelsService" />
    internal class LabelsService : LabelsCommandService, ILabelsService
    {
        internal LabelsService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <returns>The labels.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task<IEnumerable<Label>> GetAsync()
        {
            var response = await TodoistClient.GetResourcesAsync(ResourceType.Labels).ConfigureAwait(false);

            return response.Labels;
        }

        /// <summary>
        /// Gets a label info by ID.
        /// </summary>
        /// <param name="id">The ID of the label.</param>
        /// <returns>
        /// The label info.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task<LabelInfo> GetAsync(ComplexId id)
        {
            return TodoistClient.PostAsync<LabelInfo>(
                "labels/get",
                new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("label_id", id.ToString()) });
        }
    }
}
