using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents arguments for changing a user's role in a workspace.
    /// </summary>
    public class ChangeWorkspaceUserRoleArgument : DeleteWorkspaceUserArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeWorkspaceUserRoleArgument" /> class.
        /// </summary>
        /// <param name="id">The workspace identifier.</param>
        /// <param name="userEmail">The user email.</param>
        /// <param name="role">The workspace role.</param>
        public ChangeWorkspaceUserRoleArgument(ComplexId id, string userEmail, WorkspaceRole role)
            : base(id, userEmail)
        {
            Role = role;
        }

        /// <summary>
        /// Gets the workspace role.
        /// </summary>
        [JsonPropertyName("role")]
        public WorkspaceRole Role { get; }
    }
}
