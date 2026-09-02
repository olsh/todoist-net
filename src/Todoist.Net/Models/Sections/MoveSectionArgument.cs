using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a section move argument.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.MoveArgument" />
    public class MoveSectionArgument : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoveSectionArgument"/> class.
        /// </summary>
        /// <param name="id">Section identifier.</param>
        /// <param name="projectId">Id of the destination project.</param>
        public MoveSectionArgument(ComplexId id, ComplexId projectId)
        {
            Id = id;
            ProjectId = projectId;
        }

        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        [JsonPropertyName("project_id")]
        public ComplexId ProjectId { get; internal set; }
    }
}
