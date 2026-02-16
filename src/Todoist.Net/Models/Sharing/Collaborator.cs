using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist collaborator.
    /// </summary>
    public class Collaborator
    {
        [JsonConstructor]
        internal Collaborator()
        {
        }

        /// <summary>
        /// Gets the collaborator id.
        /// </summary>
        /// <value>The collaborator id.</value>
        [JsonPropertyName("id")]
        public string Id { get; internal set; }

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
        /// Gets the time zone.
        /// </summary>
        /// <value>The time zone.</value>
        [JsonPropertyName("timezone")]
        public string TimeZone { get; internal set; }

        /// <summary>
        /// Gets the image id.
        /// </summary>
        /// <value>The image id.</value>
        [JsonPropertyName("image_id")]
        public string ImageId { get; internal set; }
    }
}
