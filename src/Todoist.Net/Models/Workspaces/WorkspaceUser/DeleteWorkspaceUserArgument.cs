using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents arguments for deleting a user from a workspace.
    /// </summary>
    public class DeleteWorkspaceUserArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteWorkspaceUserArgument" /> class.
        /// </summary>
        /// <param name="id">The workspace identifier.</param>
        /// <param name="userEmail">The user email.</param>
        public DeleteWorkspaceUserArgument(ComplexId id, string userEmail)
        {
            Id = id;
            UserEmail = userEmail;
        }

        /// <summary>
        /// Gets the workspace identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public ComplexId Id { get; }

        /// <summary>
        /// Gets the user email.
        /// </summary>        
        [JsonPropertyName("user_email")]
        public string UserEmail { get; }
    }
}
