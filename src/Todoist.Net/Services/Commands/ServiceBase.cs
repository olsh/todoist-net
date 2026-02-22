using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal abstract class ServiceBase
    {
        protected ServiceBase(IAdvancedTodoistClient todoistClient)
        {
            TodoistClient = todoistClient;
        }

        protected ServiceBase()
        {
        }

        protected internal IAdvancedTodoistClient TodoistClient { get; }

        protected internal Task<SyncResponse<T>> SyncResourceAsync<T>(
            ResourceType resourceType,
            Func<SyncResourcesResponse, IReadOnlyCollection<T>> resourceSelector,
            string syncToken = "*", 
            CancellationToken cancellationToken = default) 
        {
            return SyncResourceAsync(new[] { resourceType }, resourceSelector, syncToken, cancellationToken);
        }

        protected internal async Task<SyncResponse<T>> SyncResourceAsync<T>(
            ResourceType[] resourceTypes,
            Func<SyncResourcesResponse, IReadOnlyCollection<T>> resourceSelector,
            string syncToken = "*", 
            CancellationToken cancellationToken = default)
        {
            var response = await TodoistClient.SyncResourcesAsync(resourceTypes, syncToken, cancellationToken)
                .ConfigureAwait(false);
    
            return new SyncResponse<T>(response, resourceSelector);
        }

        protected internal async Task<EntitySyncResponse<T>> SyncEntityResourceAsync<T>(
            ResourceType resourceType,
            Func<SyncResourcesResponse, T> resourceSelector,
            string syncToken = "*", 
            CancellationToken cancellationToken = default) 
        {
            var response = await TodoistClient.SyncResourcesAsync(new[] { resourceType }, syncToken, cancellationToken)
                .ConfigureAwait(false);
    
            return new EntitySyncResponse<T>(response, resourceSelector);
        }
    }
}
