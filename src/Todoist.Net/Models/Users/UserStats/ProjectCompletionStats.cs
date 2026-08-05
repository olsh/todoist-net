using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the project completion stats for a specific project.
    /// </summary>
    public class ProjectCompletionStats
    {
        /// <summary>
        /// Gets the project id.
        /// </summary>
        [JsonPropertyName("id")]
        public string ProjectId { get; internal set; }

        /// <summary>
        /// Gets the completed tasks count.
        /// </summary>
        [JsonPropertyName("completed")]
        public int CompletedCount { get; internal set; }
    }
}
