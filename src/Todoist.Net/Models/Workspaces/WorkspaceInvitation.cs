using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a workspace invitation.
    /// </summary>
    public class WorkspaceInvitation
    {
        /// <summary>
        /// Gets or sets the inviter user ID.
        /// </summary>
        [JsonPropertyName("inviter_id")]
        public string InviterId { get; set; }

        /// <summary>
        /// Gets or sets the invited user email.
        /// </summary>
        [JsonPropertyName("user_email")]
        public string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets the workspace ID.
        /// </summary>
        [JsonPropertyName("workspace_id")]
        public string WorkspaceId { get; set; }

        /// <summary>
        /// Gets or sets the workspace role.
        /// </summary>
        [JsonPropertyName("role")]
        public WorkspaceRole Role { get; set; }

        /// <summary>
        /// Gets or sets the invitation ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the invited user already exists.
        /// </summary>
        [JsonPropertyName("is_existing_user")]
        public bool IsExistingUser { get; set; }
    }
}
