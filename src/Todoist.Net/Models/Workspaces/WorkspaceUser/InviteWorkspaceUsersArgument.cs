using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents arguments for inviting users to a workspace.
    /// </summary>
    public class InviteWorkspaceUsersArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InviteWorkspaceUsersArgument" /> class.
        /// </summary>
        /// <param name="id">The workspace identifier.</param>
        /// <param name="userEmails">The user emails.</param>
        /// <param name="role">The workspace role.</param>
        public InviteWorkspaceUsersArgument(ComplexId id, string[] userEmails, WorkspaceRole role)
        {
            Id = id;
            UserEmails = userEmails;
            Role = role;
        }

        /// <summary>
        /// Gets the workspace identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public ComplexId Id { get; }

        /// <summary>
        /// Gets the user emails.
        /// </summary>
        [JsonPropertyName("user_emails")]
        public string[] UserEmails { get; }

        /// <summary>
        /// Gets the workspace role.
        /// </summary>
        [JsonPropertyName("role")]
        public WorkspaceRole Role { get; }
    }
}
