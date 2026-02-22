using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents detailed user statistics, including project colors and karma information.
    /// </summary>
    public class DetailedUserStats : UserStats
    {
        /// <summary>
        /// Gets the project colors.
        /// </summary>
        [JsonPropertyName("project_colors")]
        public Dictionary<string, Color> ProjectColors { get; internal set; }

        /// <summary>
        /// Gets the karma.
        /// </summary>
        [JsonPropertyName("karma")]
        public double Karma { get; internal set; }

        /// <summary>
        /// Gets the karma last update.
        /// </summary>
        [JsonPropertyName("karma_last_update")]
        public double KarmaLastUpdate { get; internal set; }

        /// <summary>
        /// Gets the karma trend.
        /// </summary>
        [JsonPropertyName("karma_trend")]
        public string KarmaTrend { get; internal set; }

        /// <summary>
        /// Gets the karma graph data.
        /// </summary>
        [JsonPropertyName("karma_graph_data")]
        public KarmaGraphDataItem[] KarmaGraphData { get; internal set; }

        /// <summary>
        /// Gets the karma update reasons.
        /// </summary>
        [JsonPropertyName("karma_update_reasons")]
        public KarmaUpdateReasonItem[] KarmaUpdateReasons { get; internal set; }

        /// <summary>
        /// Gets the karma goals information.
        /// </summary>
        [JsonPropertyName("goals")]
        public KarmaGoalsInfo Goals { get; internal set; }
    }
}
