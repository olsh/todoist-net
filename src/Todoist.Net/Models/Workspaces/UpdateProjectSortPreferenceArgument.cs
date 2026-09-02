using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents arguments for updating project sort preferences.
    /// </summary>
    public class UpdateProjectSortPreferenceArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProjectSortPreferenceArgument" /> class.
        /// </summary>
        /// <param name="workspaceId">The workspace identifier.</param>
        /// <param name="projectSortPreference">The project sort preference.</param>
        public UpdateProjectSortPreferenceArgument(ComplexId workspaceId, WorkspaceSortPreference projectSortPreference)
        {
            WorkspaceId = workspaceId;
            ProjectSortPreference = projectSortPreference;
        }

        /// <summary>
        /// Gets the workspace identifier.
        /// </summary>
        [JsonPropertyName("workspace_id")]
        public ComplexId WorkspaceId { get; }

        /// <summary>
        /// Gets the project sort preference.
        /// </summary>
        [JsonPropertyName("project_sort_preference")]
        public WorkspaceSortPreference ProjectSortPreference { get; }
    }
}
