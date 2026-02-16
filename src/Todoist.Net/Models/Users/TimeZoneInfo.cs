using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Class TimeZoneInfo.
    /// </summary>
    public class TimeZoneInfo
    {
        /// <summary>
        /// Gets the GMT string.
        /// </summary>
        /// <value>The GMT string.</value>
        [JsonPropertyName("gmt_string")]
        public string GmtString { get; internal set; }

        /// <summary>
        /// Gets the hours.
        /// </summary>
        /// <value>The hours.</value>
        [JsonPropertyName("hours")]
        public int Hours { get; internal set; }

        /// <summary>
        /// Gets the is DST.
        /// </summary>
        /// <value>The is DST.</value>
        [JsonPropertyName("is_dst")]
        public int IsDst { get; internal set; }

        /// <summary>
        /// Gets the minutes.
        /// </summary>
        /// <value>The minutes.</value>
        [JsonPropertyName("minutes")]
        public int Minutes { get; internal set; }

        /// <summary>
        /// Gets the timezone.
        /// </summary>
        /// <value>The timezone.</value>
        [JsonPropertyName("timezone")]
        public string Timezone { get; internal set; }
    }
}
