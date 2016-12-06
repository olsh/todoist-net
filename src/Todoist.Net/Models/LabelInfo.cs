using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents an information about a Todoist label.
    /// </summary>
    public class LabelInfo
    {
        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <value>The label.</value>
        [JsonProperty("label")]
        public Label Label { get; internal set; }
    }
}
