using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a karma graph data item.
    /// </summary>
    public class KarmaGraphDataItem
    {
        /// <summary>
        /// Gets the data point date.
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Date { get; internal set; }

        /// <summary>
        /// Gets karma value.
        /// </summary>
        [JsonPropertyName("karma_avg")]
        public int KarmaAvg { get; internal set; }
    }
}
