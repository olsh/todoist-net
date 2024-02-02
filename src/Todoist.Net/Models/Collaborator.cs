using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist collaborator.
    /// </summary>
    public class Collaborator
    {
        internal Collaborator()
        {
        }

        /// <summary>
        /// Gets the collaborator id.
        /// </summary>
        /// <value>The collaborator id.</value>
        [JsonProperty("id")]
        public string Id { get; internal set; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        [JsonProperty("email")]
        public string Email { get; internal set; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>The full name.</value>
        [JsonProperty("full_name")]
        public string FullName { get; internal set; }

        /// <summary>
        /// Gets the time zone.
        /// </summary>
        /// <value>The time zone.</value>
        [JsonProperty("timezone")]
        public string TimeZone { get; internal set; }

        /// <summary>
        /// Gets the image id.
        /// </summary>
        /// <value>The image id.</value>
        [JsonProperty("image_id")]
        public string ImageId { get; internal set; }
    }
}
