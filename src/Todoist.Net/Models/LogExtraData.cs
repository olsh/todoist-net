using System;

using Newtonsoft.Json;

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
        [JsonProperty("client")]
        public string Client { get; internal set; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>The content.</value>
        [JsonProperty("content")]
        public string Content { get; internal set; }

        /// <summary>
        /// Gets the due date.
        /// </summary>
        /// <value>The due date.</value>
        [JsonProperty("due_date")]
        public DateTime? DueDate { get; internal set; }

        /// <summary>
        /// Gets the last due date.
        /// </summary>
        /// <value>The last due date.</value>
        [JsonProperty("last_due_date")]
        public DateTime? LastDueDate { get; internal set; }
    }
}
