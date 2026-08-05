using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist collaborator.
    /// </summary>
    public class ProjectCollaborator
    {
        [JsonConstructor]
        internal ProjectCollaborator()
        {
        }

        /// <summary>
        /// Gets the collaborator id.
        /// </summary>
        /// <value>The collaborator id.</value>
        [JsonPropertyName("id")]
        public string Id { get; internal set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonPropertyName("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        [JsonPropertyName("email")]
        public string Email { get; internal set; }
    }
}
