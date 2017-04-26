using System.Collections.Generic;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents an information about a Todoist project.
    /// </summary>
    public class ProjectInfo
    {
        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <value>The notes.</value>
        [JsonProperty("notes")]
        public IReadOnlyCollection<Note> Notes { get; internal set; }

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>The project.</value>
        [JsonProperty("project")]
        public Project Project { get; internal set; }
    }
}
