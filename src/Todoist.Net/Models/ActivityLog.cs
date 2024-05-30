using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist log entry.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Gets the event date.
        /// </summary>
        /// <value>The event date.</value>
        [JsonPropertyName("event_date")]
        public DateTime EventDate { get; internal set; }

        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        /// <value>The type of the event.</value>
        [JsonPropertyName("event_type")]
        public string EventType { get; internal set; }

        /// <summary>
        /// Gets the extra data.
        /// </summary>
        /// <value>The extra data.</value>
        [JsonPropertyName("extra_data")]
        public LogExtraData ExtraData { get; internal set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonPropertyName("id")]
        public long Id { get; internal set; }

        /// <summary>
        /// Gets the initiator identifier.
        /// </summary>
        /// <value>The initiator identifier.</value>
        [JsonPropertyName("initiator_id")]
        public long? InitiatorId { get; internal set; }

        /// <summary>
        /// Gets the object identifier.
        /// </summary>
        /// <value>The object identifier.</value>
        [JsonPropertyName("object_id")]
        public long ObjectId { get; internal set; }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        [JsonPropertyName("object_type")]
        public string ObjectType { get; internal set; }

        /// <summary>
        /// Gets the parent item identifier.
        /// </summary>
        /// <value>The parent item identifier.</value>
        [JsonPropertyName("parent_item_id")]
        public long? ParentItemId { get; internal set; }

        /// <summary>
        /// Gets the parent project identifier.
        /// </summary>
        /// <value>The parent project identifier.</value>
        [JsonPropertyName("parent_project_id")]
        public long? ParentProjectId { get; internal set; }
    }
}
