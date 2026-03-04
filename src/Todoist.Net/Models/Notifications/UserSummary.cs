using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a user summary with additional avatar information for sharing and collaboration contexts.
    /// </summary>
    public class UserSummary
    {
        [JsonConstructor]
        internal UserSummary()
        {
        }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        /// <value>The user id.</value>
        [JsonPropertyName("id")]
        public long Id { get; internal set; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        [JsonPropertyName("email")]
        public string Email { get; internal set; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>The full name.</value>
        [JsonPropertyName("full_name")]
        public string FullName { get; internal set; }

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
