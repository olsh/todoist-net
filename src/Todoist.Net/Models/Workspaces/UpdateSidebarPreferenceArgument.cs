using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents arguments for updating sidebar preferences.
    /// </summary>
    public class UpdateSidebarPreferenceArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSidebarPreferenceArgument" /> class.
        /// </summary>
        /// <param name="workspaceId">The workspace identifier.</param>
        /// <param name="sidebarPreference">The sidebar preference.</param>
        public UpdateSidebarPreferenceArgument(ComplexId workspaceId, WorkspaceSortPreference sidebarPreference)
        {
            WorkspaceId = workspaceId;
            SidebarPreference = sidebarPreference;
        }

        /// <summary>
        /// Gets the workspace identifier.
        /// </summary>
        [JsonPropertyName("workspace_id")]
        public ComplexId WorkspaceId { get; }

        /// <summary>
        /// Gets the sidebar preference.
        /// </summary>
        [JsonPropertyName("sidebar_preference")]
        public WorkspaceSortPreference SidebarPreference { get; }
    }
}
