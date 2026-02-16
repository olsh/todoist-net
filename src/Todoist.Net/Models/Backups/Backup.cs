using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist backup.
    /// </summary>
    public class Backup
    {
        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [JsonPropertyName("url")]
        public string Url { get; internal set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        [JsonPropertyName("version")]
        public string Version { get; internal set; }
    }
}
