using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a karma update reason item.
    /// </summary>
    public class KarmaUpdateReasonItem
    {
        /// <summary>
        /// Gets the change date.
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Time { get; internal set; }

        /// <summary>
        /// Gets negative karma value.
        /// </summary>
        [JsonPropertyName("negative_karma")]
        public double NegativeKarma { get; internal set; }

        /// <summary>
        /// Gets negative karma reasons.
        /// </summary>
        [JsonPropertyName("negative_karma_reasons")]
        public int[] NegativeKarmaReasons { get; internal set; }

        /// <summary>
        /// Gets positive karma value.
        /// </summary>
        [JsonPropertyName("positive_karma")]
        public double PositiveKarma { get; internal set; }

        /// <summary>
        /// Gets positive karma reasons.
        /// </summary>
        [JsonPropertyName("positive_karma_reasons")]
        public int[] PositiveKarmaReasons { get; internal set; }

        /// <summary>
        /// Gets new karma value.
        /// </summary>
        [JsonPropertyName("new_karma")]
        public double NewKarma { get; internal set; }
    }
}
