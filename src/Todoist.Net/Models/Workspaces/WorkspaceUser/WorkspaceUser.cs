using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a workspace user entry.
    /// </summary>
    public class WorkspaceUser : UserSummary
    {
        /// <summary>
        /// Gets the user ID.
        /// </summary>
        [JsonPropertyName("user_id")]
        internal string UserId 
        { 
            get => Id;
            set => Id = value;
        }

        /// <summary>
        /// Gets the user email.
        /// </summary>
        [JsonPropertyName("user_email")]
        internal string UserEmail 
        { 
            get => Email;
            set => Email = value; 
        }

        /// <summary>
        /// Gets the workspace ID.
        /// </summary>
        [JsonPropertyName("workspace_id")]
        public string WorkspaceId { get; internal set; }

        /// <summary>
        /// Gets the timezone.
        /// </summary>
        [JsonPropertyName("timezone")]
        public string Timezone { get; internal set; }

        /// <summary>
        /// Gets the role.
        /// </summary>
        [JsonPropertyName("role")]
        public WorkspaceRole Role { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this user is deleted.
        /// </summary>
        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; internal set; }
    }
}
