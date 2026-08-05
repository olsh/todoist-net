using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents available project and workspace collaborator actions by role.
    /// </summary>
    public class ProjectPermissions
    {
        /// <summary>
        /// Gets or sets project collaborator actions by role.
        /// </summary>
        [JsonPropertyName("project_collaborator_actions")]
        public IReadOnlyCollection<ProjectPermissionRoleActions> ProjectCollaboratorActions { get; set; }

        /// <summary>
        /// Gets or sets workspace collaborator actions by role.
        /// </summary>
        [JsonPropertyName("workspace_collaborator_actions")]
        public IReadOnlyCollection<ProjectPermissionRoleActions> WorkspaceCollaboratorActions { get; set; }
    }

    /// <summary>
    /// Represents an action list for a specific collaborator role.
    /// </summary>
    public class ProjectPermissionRoleActions
    {
        /// <summary>
        /// Gets or sets collaborator role name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets actions allowed for the role.
        /// </summary>
        [JsonPropertyName("actions")]
        public IReadOnlyCollection<ProjectPermissionAction> Actions { get; set; }
    }

    /// <summary>
    /// Represents a single allowed action.
    /// </summary>
    public class ProjectPermissionAction
    {
        /// <summary>
        /// Gets or sets action name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
