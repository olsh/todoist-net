using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the user stats for a specific week.
    /// </summary>
    public class WeekUserStats
    {
        /// <summary>
        /// Gets the start date of the week.
        /// </summary>
        [JsonPropertyName("from")]
        public DateTime FromDate { get; internal set; }

        /// <summary>
        /// Gets the end date of the week.
        /// </summary>
        [JsonPropertyName("to")]
        public DateTime ToDate { get; internal set; }

        /// <summary>
        /// Gets the total completed tasks count for the week.
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
