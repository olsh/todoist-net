using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the base response for synchronization operations.
    /// </summary>
    public class BaseSyncResponse
    {
        /// <summary>
        /// Gets the synchronization token for future sync requests.
        /// </summary>
        /// <remarks>
        /// This token should be used in subsequent synchronization requests to fetch only the changes since this sync.
        /// </remarks>
        [JsonPropertyName("sync_token")]
        public string SyncToken { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this synchronization response is a full sync.
        /// </summary>
        [JsonPropertyName("full_sync")]
        public bool FullSync { get; internal set; }

        /// <summary>
        /// Gets the date and time of the last full synchronization in UTC.
        /// </summary>
        [JsonPropertyName("full_sync_date_utc")]
        public DateTime? FullSyncDateUtc { get; internal set; }
    }
}
