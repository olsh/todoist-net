using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents an information about a Todoist filter.
    /// </summary>
    public class FilterInfo
    {
        /// <summary>
        /// Gets the filter.
        /// </summary>
        /// <value>The filter.</value>
        [JsonProperty("filter")]
        public Filter Filter { get; internal set; }
    }
}
