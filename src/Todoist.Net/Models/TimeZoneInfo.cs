using Newtonsoft.Json;

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
        [JsonProperty("gmt_string")]
        public string GmtString { get; internal set; }

        /// <summary>
        /// Gets the hours.
        /// </summary>
        /// <value>The hours.</value>
        [JsonProperty("hours")]
        public int Hours { get; internal set; }

        /// <summary>
        /// Gets the is DST.
        /// </summary>
        /// <value>The is DST.</value>
        [JsonProperty("is_dst")]
        public int IsDst { get; internal set; }

        /// <summary>
        /// Gets the minutes.
        /// </summary>
        /// <value>The minutes.</value>
        [JsonProperty("minutes")]
        public int Minutes { get; internal set; }

        /// <summary>
        /// Gets the timezone.
        /// </summary>
        /// <value>The timezone.</value>
        [JsonProperty("timezone")]
        public string Timezone { get; internal set; }
    }
}
