using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a workspace user entry.
    /// </summary>
    public class WorkspaceUser
    {
        /// <summary>
        /// Gets the user ID.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; internal set; }

        /// <summary>
        /// Gets the user email.
        /// </summary>
        [JsonPropertyName("user_email")]
        public string UserEmail { get; internal set; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>The full name.</value>
        [JsonPropertyName("full_name")]
        public string FullName { get; internal set; }

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

        /// <summary>
        /// Gets the image id.
        /// </summary>
        /// <value>The image id.</value>
        [JsonPropertyName("image_id")]
        public string ImageId { get; internal set; }

        /// <summary>
        /// Gets The link to a 195x195 pixels image of the user's avatar.
        /// </summary>
        [JsonPropertyName("avatar_big")]
        public string AvatarBig { get; internal set; }

        /// <summary>
        /// Gets The link to a 60x60 pixels image of the user's avatar.
        /// </summary>
        [JsonPropertyName("avatar_medium")]
        public string AvatarMedium { get; internal set; }

        /// <summary>
        /// Gets The link to a 35x35  pixels image of the user's avatar.
        /// </summary>
        [JsonPropertyName("avatar_small")]
        public string AvatarSmall { get; internal set; }

        /// <summary>
        /// Gets the link to a 640x640 pixels image of the user's avatar.
        /// </summary>
        [JsonPropertyName("avatar_s640")]
        public string AvatarS640 { get; internal set; }
    }
}
