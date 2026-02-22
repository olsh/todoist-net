using System.Text.Json.Serialization;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents an argument containing a project identifier.
    /// </summary>
    public class ProjectIdArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectIdArgument" /> class.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        public ProjectIdArgument(ComplexId projectId)
        {
            ThrowHelper.ThrowIfDefaultOrEmpty(projectId, nameof(projectId));

            ProjectId = projectId;
        }

        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        [JsonPropertyName("project_id")]
        public ComplexId ProjectId { get; }
    }
}
