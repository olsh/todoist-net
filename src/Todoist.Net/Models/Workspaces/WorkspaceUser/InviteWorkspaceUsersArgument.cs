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
        /// <param name="emailList">The user emails.</param>
        /// <param name="role">The workspace role.</param>
        public InviteWorkspaceUsersArgument(ComplexId id, string[] emailList, WorkspaceRole role)
        {
            Id = id;
            EmailList = emailList;
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
        [JsonPropertyName("email_list")]
        public string[] EmailList { get; }

        /// <summary>
        /// Gets the workspace role.
        /// </summary>
        [JsonPropertyName("role")]
        public WorkspaceRole Role { get; }
    }
}
