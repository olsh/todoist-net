using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents activity.
    /// </summary>
    public class Activity
    {
        internal Activity()
        {
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        [JsonProperty("count")]
        public long Count { get; internal set; }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <value>
        /// The events.
        /// </value>
        [JsonProperty("events")]
        public LogEntry[] Events { get; internal set; }
    }
}
