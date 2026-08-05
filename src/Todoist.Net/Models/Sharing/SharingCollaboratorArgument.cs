using System.Text.Json.Serialization;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    internal class SharingCollaboratorArgument : ICommandArgument
    {
        internal SharingCollaboratorArgument(ComplexId projectId, string email, ProjectCollaboratorRole role = null)
        {
            ThrowHelper.ThrowIfDefaultOrEmpty(projectId, nameof(projectId));
            ThrowHelper.ThrowIfNullOrEmpty(email, nameof(email));

            ProjectId = projectId;
            Email = email;
            Role = role;
        }

        [JsonPropertyName("project_id")]
        public ComplexId ProjectId { get; }

        [JsonPropertyName("email")]
        public string Email { get; }

        [JsonPropertyName("role")]
        public ProjectCollaboratorRole Role { get; }
    }
}
