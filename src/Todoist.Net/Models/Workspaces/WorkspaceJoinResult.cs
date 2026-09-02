using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the join-workspace response payload.
    /// </summary>
    public class WorkspaceJoinResult
    {
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the workspace ID.
        /// </summary>
        [JsonPropertyName("workspace_id")]
        public string WorkspaceId { get; set; }

        /// <summary>
        /// Gets or sets the workspace role.
        /// </summary>
        [JsonPropertyName("role")]
        public WorkspaceRole Role { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether custom sorting is applied.
        /// </summary>
        [JsonPropertyName("custom_sorting_applied")]
        public bool CustomSortingApplied { get; set; }

        /// <summary>
        /// Gets or sets the project sort preference.
        /// </summary>
        [JsonPropertyName("project_sort_preference")]
        public WorkspaceSortPreference ProjectSortPreference { get; set; }
    }
}
