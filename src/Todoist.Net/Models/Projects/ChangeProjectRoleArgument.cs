using System.Text.Json.Serialization;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a project role change command payload.
    /// </summary>
    public class ChangeProjectRoleArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeProjectRoleArgument" /> class.
        /// </summary>
        /// <param name="id">The project identifier.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="role">The project role.</param>
        public ChangeProjectRoleArgument(ComplexId id, long userId, ProjectCollaboratorRole role)
        {
            ThrowHelper.ThrowIfDefaultOrEmpty(id, nameof(id));

            Id = id;
            UserId = userId;
            Role = role;
        }

        /// <summary>
        /// Gets or sets the project ID.
        /// </summary>
        [JsonPropertyName("id")]
        public ComplexId Id { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        [JsonPropertyName("role")]
        public ProjectCollaboratorRole Role { get; set; }
    }
}
