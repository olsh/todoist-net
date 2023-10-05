using System.Collections.Generic;
using System.Threading;
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

        /// <inheritdoc/>
        public async Task<IEnumerable<Label>> GetAsync(CancellationToken cancellationToken = default)
        {
            var response = await TodoistClient.GetResourcesAsync(cancellationToken, ResourceType.Labels).ConfigureAwait(false);

            return response.Labels;
        }

        /// <inheritdoc/>
        public Task<LabelInfo> GetAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return TodoistClient.PostAsync<LabelInfo>(
                "labels/get",
                new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("label_id", id.ToString()) },
                cancellationToken);
        }
    }
}
