using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a goal streak item.
    /// </summary>
    public class KarmaGoalStreakItem
    {
        /// <summary>
        /// Gets the streak start date.
        /// </summary>
        [JsonPropertyName("start")]
        public DateTime Start { get; internal set; }

        /// <summary>
        /// Gets the streak end date.
        /// </summary>
        [JsonPropertyName("end")]
        public DateTime End { get; internal set; }

        /// <summary>
        /// Gets the streak length/count.
        /// </summary>
        [JsonPropertyName("count")]
        public int Count { get; internal set; }
    }
}
