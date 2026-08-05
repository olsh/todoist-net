using System.Text.Json.Serialization;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents arguments for moving a project to another workspace.
    /// </summary>
    public class MoveProjectToWorkspaceArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoveProjectToWorkspaceArgument" /> class.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="workspaceId">The workspace identifier.</param>
        /// <param name="folderId">The folder identifier.</param>
        /// <param name="isInviteOnly">A value indicating whether the workspace is invite-only.</param>
        public MoveProjectToWorkspaceArgument(ComplexId projectId, ComplexId workspaceId, ComplexId? folderId = null, bool? isInviteOnly = null)
        {
            ThrowHelper.ThrowIfDefaultOrEmpty(projectId, nameof(projectId));
            ThrowHelper.ThrowIfDefaultOrEmpty(workspaceId, nameof(workspaceId));

            ProjectId = projectId;
            WorkspaceId = workspaceId;
            FolderId = folderId;
            IsInviteOnly = isInviteOnly;
        }

        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        [JsonPropertyName("project_id")]
        public ComplexId ProjectId { get; }

        /// <summary>
        /// Gets the workspace identifier.
        /// </summary>
        [JsonPropertyName("workspace_id")]
        public ComplexId WorkspaceId { get; }

        /// <summary>
        /// Gets the folder identifier.
        /// </summary>
        [JsonPropertyName("folder_id")]
        public ComplexId? FolderId { get; }

        /// <summary>
        /// Gets a value indicating whether the workspace is invite-only.
        /// </summary>
        [JsonPropertyName("is_invite_only")]
        public bool? IsInviteOnly { get; }
    }
}
