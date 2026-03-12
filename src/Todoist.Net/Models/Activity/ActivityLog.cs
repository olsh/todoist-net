using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist activity log entry.
    /// </summary>
    public class ActivityLog
    {
        /// <summary>
        /// Gets the object identifier.
        /// </summary>
        [JsonPropertyName("object_id")]
        public string ObjectId { get; internal set; }

        /// <summary>
        /// Gets the type of the activity object.
        /// </summary>
        [JsonPropertyName("object_type")]
        public LogObjectType ObjectType { get; internal set; }

        /// <summary>
        /// Gets the type of the activity event.
        /// </summary>
        [JsonPropertyName("event_type")]
        public LogEventType EventType { get; internal set; }

        /// <summary>
        /// Gets the date of the activity.
        /// </summary>
        [JsonPropertyName("event_date")]
        public DateTime EventDate { get; internal set; }

        /// <summary>
        /// Gets the source of the activity.
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; internal set; }

        /// <summary>
        /// Gets the parent project identifier.
        /// </summary>
        [JsonPropertyName("parent_project_id")]
        public string ParentProjectId { get; internal set; }

        /// <summary>
        /// Gets the parent item identifier.
        /// </summary>
        [JsonPropertyName("parent_item_id")]
        public string ParentItemId { get; internal set; }

        /// <summary>
        /// Gets the initiator identifier.
        /// </summary>
        [JsonPropertyName("initiator_id")]
        public string InitiatorId { get; internal set; }

        /// <summary>
        /// Gets the extra data.
        /// </summary>
        /// <value>The extra data.</value>
        [JsonPropertyName("extra_data")]
        public Dictionary<string, object> ExtraData { get; internal set; }
    }
}
