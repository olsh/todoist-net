using System;
using System.Collections.Generic;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a response that contains synchronized data of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the synchronized data included in the response.</typeparam>
    public class SyncResponse<T> : BaseSyncResponse
    {
        internal SyncResponse(SyncResourcesResponse response, Func<SyncResourcesResponse, IReadOnlyCollection<T>> dataSelector)
        {
            SyncToken = response.SyncToken;
            FullSync = response.FullSync;
            FullSyncDateUtc = response.FullSyncDateUtc;

            Data = dataSelector(response);
        }

        /// <summary>
        /// Gets the synchronized data of type <typeparamref name="T"/>.
        /// </summary>
        public IReadOnlyCollection<T> Data { get; internal set; }
    }
}
