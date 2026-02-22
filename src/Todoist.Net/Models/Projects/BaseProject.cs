using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a base Todoist project.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.BaseUnsetEntity" />
    public class BaseProject : BaseUnsetEntity
    {
        private protected BaseProject(ComplexId id)
            : base(id)
        {
        }

        private protected BaseProject()
        {
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        [JsonPropertyName("color")]
        public Color Color { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is favorite.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is favorite; otherwise, <c>false</c>.
        /// </value>
        [JsonPropertyName("is_favorite")]
        public bool IsFavorite { get; set; }

        /// <summary>
        /// Gets the project view style.
        /// </summary>
        [JsonPropertyName("view_style")]
        public ViewOptionsStyle ViewStyle { get; set; }

        /// <summary>
        /// Gets the project description.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets the project status.
        /// </summary>
        [JsonPropertyName("status")]
        public ProjectStatus Status { get; set; }

        /// <summary>
        /// Gets a value indicating whether link sharing is enabled for the project.
        /// </summary>
        [JsonPropertyName("is_link_sharing_enabled")]
        public bool? IsLinkSharingEnabled { get; set; }

        /// <summary>
        /// Gets the default collaborator role for new project collaborators.
        /// </summary>
        [JsonPropertyName("collaborator_role_default")]
        public ProjectCollaboratorRole CollaboratorRoleDefault { get; set; }

        /// <summary>
        /// Gets project access visibility/configuration.
        /// </summary>
        [JsonPropertyName("access")]
        public SharedProjectAccess Access { get; set; }
    }
}
