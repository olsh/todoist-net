using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents an information about a Todoist task.
    /// </summary>
    public class ItemInfo
    {
        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <value>The item.</value>
        [JsonPropertyName("item")]
        public Item Item { get; internal set; }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <value>The notes.</value>
        [JsonPropertyName("notes")]
        public IReadOnlyCollection<Note> Notes { get; internal set; }

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>The project.</value>
        [JsonPropertyName("project")]
        public Project Project { get; internal set; }
    }
}
