using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents default collaborators automatically added to new workspace projects.
    /// </summary>
    public class WorkspaceDefaultCollaborators
    {
        /// <summary>
        /// Gets or sets the default collaborator user ids.
        /// </summary>
        [JsonPropertyName("user_ids")]
        public ICollection<long> UserIds { get; set; }

        /// <summary>
        /// Gets or sets predefined group ids.
        /// </summary>
        [JsonPropertyName("predefined_group_ids")]
        public ICollection<string> PredefinedGroupIds { get; set; }
    }
}