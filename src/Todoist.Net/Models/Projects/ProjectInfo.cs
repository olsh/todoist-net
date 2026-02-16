using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents an information about a Todoist project.
    /// </summary>
    public class ProjectInfo
    {
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
