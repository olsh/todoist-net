using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a paginated workspace projects response.
    /// </summary>
    public class PaginatedWorkspaceProjects
    {
        /// <summary>
        /// Gets or sets a value indicating whether there are more projects available.
        /// </summary>
        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }

        /// <summary>
        /// Gets or sets the cursor for the next page.
        /// </summary>
        [JsonPropertyName("next_cursor")]
        public string NextCursor { get; set; }

        /// <summary>
        /// Gets or sets the workspace projects for the current page.
        /// </summary>
        [JsonPropertyName("workspace_projects")]
        public IReadOnlyList<WorkspaceProject> WorkspaceProjects { get; set; }
    }
}
