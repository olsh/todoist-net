using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the user statistics, including completed tasks count and daily/weekly stats.
    /// </summary>
    public class UserStats
    {
        /// <summary>
        /// Gets the completed tasks count.
        /// </summary>
        [JsonPropertyName("completed_count")]
        public int CompletedCount { get; internal set; }

        /// <summary>
        /// Gets the daily user stats.
        /// </summary>
        [JsonPropertyName("days_items")]
        public DayUserStats[] DaysItems { get; internal set; }

        /// <summary>
        /// Gets the weekly user stats.
        /// </summary>
        [JsonPropertyName("week_items")]
        public WeekUserStats[] WeekItems { get; internal set; }
    }
}
