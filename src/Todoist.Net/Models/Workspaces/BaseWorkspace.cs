using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents common workspace data used by add, update, and read operations.
    /// </summary>
    public class BaseWorkspace : BaseUnsetEntity
    {
        private protected BaseWorkspace(ComplexId id)
            : base(id)
        {
        }

        private protected BaseWorkspace()
        {
        }

        /// <summary>
        /// Gets or sets the workspace name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the workspace description.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether link sharing is enabled.
        /// </summary>
        [JsonPropertyName("is_link_sharing_enabled")]
        public bool? IsLinkSharingEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether guests are allowed.
        /// </summary>
        [JsonPropertyName("is_guest_allowed")]
        public bool? IsGuestAllowed { get; set; }

        /// <summary>
        /// Gets or sets the workspace domain name.
        /// </summary>
        [JsonPropertyName("domain_name")]
        public string DomainName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether users in the workspace domain can join without invitation.
        /// </summary>
        [JsonPropertyName("domain_discovery")]
        public bool? DomainDiscovery { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether only users in the workspace domain are allowed.
        /// </summary>
        [JsonPropertyName("restrict_email_domains")]
        public bool? RestrictEmailDomains { get; set; }

        /// <summary>
        /// Gets or sets the workspace configuration properties.
        /// </summary>
        [JsonPropertyName("properties")]
        public WorkspaceProperties Properties { get; set; }

        /// <summary>
        /// Gets or sets default collaborators for new projects.
        /// </summary>
        [JsonPropertyName("default_collaborators")]
        public WorkspaceDefaultCollaborators DefaultCollaborators { get; set; }

    }
}
