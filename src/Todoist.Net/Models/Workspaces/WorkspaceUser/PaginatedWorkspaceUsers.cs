using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a paginated workspace users response.
    /// </summary>
    public class PaginatedWorkspaceUsers
    {
        /// <summary>
        /// Gets or sets a value indicating whether there are more users available.
        /// </summary>
        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }

        /// <summary>
        /// Gets or sets the cursor for the next page.
        /// </summary>
        [JsonPropertyName("next_cursor")]
        public string NextCursor { get; set; }

        /// <summary>
        /// Gets or sets the workspace users for the current page.
        /// </summary>
        [JsonPropertyName("workspace_users")]
        public IReadOnlyList<WorkspaceUser> WorkspaceUsers { get; set; }
    }
}
