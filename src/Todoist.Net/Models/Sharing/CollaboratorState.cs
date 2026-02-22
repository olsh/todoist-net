using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist collaborator state.
    /// </summary>
    public class CollaboratorState
    {
        [JsonConstructor]
        internal CollaboratorState()
        {
        }

        /// <summary>
        /// Gets the user id of the collaborator.
        /// </summary>
        /// <value>The user id.</value>
        [JsonPropertyName("user_id")]
        public string UserId { get; internal set; }

        /// <summary>
        /// Gets the shared project id.
        /// </summary>
        /// <value>The project id.</value>
        [JsonPropertyName("project_id")]
        public string ProjectId { get; internal set; }

        /// <summary>
        /// Gets the collaborator status.
        /// </summary>
        /// <value>The status.</value>
        [JsonPropertyName("state")]
        public string State { get; internal set; }

        /// <summary>
        /// Gets the collaborator role.
        /// </summary>
        /// <value>The role.</value>
        [JsonPropertyName("role")]
        public string Role { get; internal set; }

        /// <summary>
        /// Gets the collaborator workspace role.
        /// </summary>
        /// <value>The workspace role.</value>
        [JsonPropertyName("workspace_role")]
        public string WorkspaceRole { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this collaborator is deleted.
        /// </summary>
        /// <value><c>true</c> if this collaborator is deleted; otherwise, <c>false</c>.</value>
        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; internal set; }

    }
}
