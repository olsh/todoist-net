using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents information about completed tasks.
    /// </summary>
    public class CompletedTasksInfo
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
        public IReadOnlyCollection<CompletedTask> Tasks { get; internal set; }

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
