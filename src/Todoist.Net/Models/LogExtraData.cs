using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents extra data of a Todoist log entry.
    /// </summary>
    public class LogExtraData
    {
        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        [JsonPropertyName("client")]
        public string Client { get; internal set; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>The content.</value>
        [JsonPropertyName("content")]
        public string Content { get; internal set; }

        /// <summary>
        /// Gets the due date.
        /// </summary>
        /// <value>The due date.</value>
        [JsonPropertyName("due_date")]
        public DateTime? DueDate { get; internal set; }

        /// <summary>
        /// Gets the last due date.
        /// </summary>
        /// <value>The last due date.</value>
        [JsonPropertyName("last_due_date")]
        public DateTime? LastDueDate { get; internal set; }
    }
}
