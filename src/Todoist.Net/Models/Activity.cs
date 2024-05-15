using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents activity.
    /// </summary>
    public class Activity
    {
        [JsonConstructor]
        internal Activity()
        {
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        [JsonPropertyName("count")]
        public long Count { get; internal set; }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <value>
        /// The events.
        /// </value>
        [JsonPropertyName("events")]
        public LogEntry[] Events { get; internal set; }
    }
}
