using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents common workspace project data.
    /// </summary>
    public class WorkspaceProject : ProjectInfo
    {
        /// <summary>
        /// Gets the initiated-by user id.
        /// </summary>
        [JsonPropertyName("initiated_by_uid")]
        public long? InitiatedByUid { get; set; }

        /// <summary>
        /// Gets the archived timestamp.
        /// </summary>
        [JsonPropertyName("archived_timestamp")]
        public long? ArchivedTimestamp { get; set; }

        /// <summary>
        /// Gets the archived date.
        /// </summary>
        [JsonPropertyName("archived_date")]
        public DateTime? ArchivedDate { get; set; }
    }
}
