using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the access object of a project.
    /// </summary>
    public class SharedProjectAccess
    {
        /// <summary>
        /// Gets the project visibility.
        /// </summary>
        [JsonPropertyName("visibility")]
        public ProjectAccessVisibility Visibility { get; internal set; }

        /// <summary>
        /// Gets project access configuration.
        /// </summary>
        [JsonPropertyName("configuration")]
        public ProjectAccessConfiguration Configuration { get; internal set; }
    }
}
