using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the user stats for a specific day.
    /// </summary>
    public class DayUserStats
    {
        /// <summary>
        /// Gets the date.
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Date { get; internal set; }

        /// <summary>
        /// Gets the total completed tasks count.
        /// </summary>
        [JsonPropertyName("total_completed")]
        public int TotalCompletedCount { get; internal set; }

        /// <summary>
        /// Gets the project completion stats for the day.
        /// </summary>
        [JsonPropertyName("items")]
        public ProjectCompletionStats[] Items { get; internal set; }
    }
}
