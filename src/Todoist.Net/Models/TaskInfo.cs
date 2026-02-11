using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents information about a Todoist task.
    /// </summary>
    public class TaskInfo
    {
        /// <summary>
        /// Gets the task.
        /// </summary>
        /// <value>The task.</value>
        /// <remarks>
        /// The JSON property name remains "item" for backwards compatibility with Sync API.
        /// </remarks>
        [JsonPropertyName("item")]
        public DetailedTask Task { get; internal set; }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <value>The comments.</value>
        /// <remarks>
        /// The JSON property name remains "notes" for backwards compatibility with Sync API.
        /// </remarks>
        [JsonPropertyName("notes")]
        public IReadOnlyCollection<Comment> Comments { get; internal set; }

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>The project.</value>
        [JsonPropertyName("project")]
        public Project Project { get; internal set; }
    }
}
