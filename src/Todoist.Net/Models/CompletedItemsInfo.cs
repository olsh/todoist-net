using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents information about completed tasks.
    /// </summary>
    public class CompletedItemsInfo
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [JsonPropertyName("items")]
        public IReadOnlyCollection<CompletedItem> Items { get; internal set; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        [JsonPropertyName("projects")]
        public IReadOnlyDictionary<long, Project> Projects { get; internal set; }
    }
}
