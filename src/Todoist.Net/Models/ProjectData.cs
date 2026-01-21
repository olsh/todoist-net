using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist project and its uncompleted tasks.
    /// </summary>
    public class ProjectData
    {
        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <value>
        /// The tasks.
        /// </value>
        /// <remarks>
        /// The JSON property name remains "items" for backwards compatibility with Sync API.
        /// </remarks>
        [JsonPropertyName("items")]
        public IReadOnlyCollection<DetailedTask> Tasks { get; internal set; }

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>The project.</value>
        [JsonPropertyName("project")]
        public Project Project { get; internal set; }
    }
}
