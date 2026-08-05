using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents project access configuration details.
    /// </summary>
    public class ProjectAccessConfiguration
    {
        /// <summary>
        /// Gets a value indicating whether collaborator details are hidden for public access.
        /// </summary>
        [JsonPropertyName("hide_collaborator_details")]
        public bool? HideCollaboratorDetails { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether project duplication is disabled for public access.
        /// </summary>
        [JsonPropertyName("disable_duplication")]
        public bool? DisableDuplication { get; internal set; }

    }
}
