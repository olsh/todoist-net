using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal class IdsService : ServiceBase, IIdsService
    {
        internal IdsService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<IReadOnlyCollection<IdMapping>> GetMappingsAsync(
            MappingObjectName objectName, 
            ICollection<string> objectIds, 
            CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNull(objectName, nameof(objectName));
            ThrowHelper.ThrowIfNullOrEmpty(objectIds, nameof(objectIds));

            return TodoistClient.GetAsync<IReadOnlyCollection<IdMapping>>(
                $"id_mappings/{objectName}/{string.Join(",", objectIds)}", 
                cancellationToken: cancellationToken);
        }
    }
}
