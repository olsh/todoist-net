using System;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a response that contains a synchronized entity of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the synchronized entity included in the response.</typeparam>
    public class EntitySyncResponse<T> : BaseSyncResponse
    {
        internal EntitySyncResponse(SyncResourcesResponse response, Func<SyncResourcesResponse, T> dataSelector)
        {
            SyncToken = response.SyncToken;
            FullSync = response.FullSync;
            FullSyncDateUtc = response.FullSyncDateUtc;

            Value = dataSelector(response);
        }

        /// <summary>
        /// Gets the synchronized value of type <typeparamref name="T"/>.
        /// </summary>
        public T Value { get; internal set; }
    }
}
