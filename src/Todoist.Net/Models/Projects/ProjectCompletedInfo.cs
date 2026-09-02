using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents statistics about completed tasks in a project.
    /// </summary>
    public class ProjectCompletedInfo
    {
        /// <summary>
        /// Gets the project ID.
        /// </summary>
        /// <value>The project ID.</value>
        [JsonPropertyName("project_id")]
        public string ProjectId { get; internal set; }

        /// <summary>
        /// Gets the archived sections count.
        /// </summary>
        /// <value>The archived sections count.</value>
        [JsonPropertyName("archived_sections")]
        public int ArchivedSections { get; internal set; }

        /// <summary>
        /// Gets the completed tasks count.
        /// </summary>
        /// <value>The completed tasks count.</value>
        /// <remarks>The JSON property name remains "completed_items" for backwards compatibility with Sync API.</remarks>
        [JsonPropertyName("completed_items")]
        public int CompletedTasks { get; internal set; }
    }
}
