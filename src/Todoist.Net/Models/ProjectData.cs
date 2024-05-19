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
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [JsonPropertyName("items")]
        public IReadOnlyCollection<Item> Items { get; internal set; }

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>The project.</value>
        [JsonPropertyName("project")]
        public Project Project { get; internal set; }
    }
}
