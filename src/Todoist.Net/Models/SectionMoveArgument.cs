using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a section move argument.
    /// </summary>
    public class SectionMoveArgument : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SectionMoveArgument" /> class.
        /// </summary>
        /// <param name="id">The identifier of moved entity.</param>
        /// <param name="projectId">Id of the destination project.</param>
        /// <exception cref="T:System.ArgumentException">Entity ID is required for the operation</exception>
        public SectionMoveArgument(ComplexId id, ComplexId? projectId)
            : base(id)
        {
            if (id.IsEmpty)
            {
                throw new ArgumentException("Entity ID is required for the move operation", nameof(id));
            }

            ProjectId = projectId;
        }

        [JsonConstructor]
        internal SectionMoveArgument()
        {
        }

        /// <summary>Gets the parent entity identifier.</summary>
        /// <value>The parent entity identifier.</value>
        [JsonPropertyName("project_id")]
        public ComplexId? ProjectId { get; internal set; }
    }
}
