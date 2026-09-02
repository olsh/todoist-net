using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents configuration properties for a workspace.
    /// </summary>
    public class WorkspaceProperties
    {
        /// <summary>
        /// Gets or sets the industry of the workspace.
        /// </summary>
        [JsonPropertyName("industry")]
        public WorkspaceIndustry Industry { get; set; }

        /// <summary>
        /// Gets or sets the department of the workspace.
        /// </summary>
        [JsonPropertyName("department")]
        public WorkspaceDepartment Department { get; set; }

        /// <summary>
        /// Gets or sets the organization size.
        /// </summary>
        [JsonPropertyName("organization_size")]
        public WorkspaceOrganizationSize OrganizationSize { get; set; }

        /// <summary>
        /// Gets or sets the creator role.
        /// </summary>
        [JsonPropertyName("creator_role")]
        public WorkspaceCreatorRole CreatorRole { get; set; }

        /// <summary>
        /// Gets or sets the two-letter continent code.
        /// </summary>
        [JsonPropertyName("region")]
        public WorkspaceRegion Region { get; set; }

        /// <summary>
        /// Gets or sets the two-letter ISO country code.
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the default access level for new projects.
        /// </summary>
        [JsonPropertyName("default_access_level")]
        public WorkspaceDefaultAccessLevel DefaultAccessLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether beta features are enabled.
        /// </summary>
        [JsonPropertyName("beta_enabled")]
        public bool? BetaEnabled { get; set; }

        /// <summary>
        /// Gets or sets the acquisition source.
        /// </summary>
        [JsonPropertyName("acquisition_source")]
        public WorkspaceAcquisitionSource AcquisitionSource { get; set; }

        /// <summary>
        /// Gets or sets how the creator heard about Todoist.
        /// </summary>
        [JsonPropertyName("hdyhau")]
        public WorkspaceHearAboutSource Hdyhau { get; set; }

        /// <summary>
        /// Gets or sets the onboarding filter ID.
        /// </summary>
        [JsonPropertyName("onboarding_filter_id")]
        public long? OnboardingFilterId { get; set; }
    }
}