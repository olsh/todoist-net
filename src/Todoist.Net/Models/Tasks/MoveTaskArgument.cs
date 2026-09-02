using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a task move argument.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.MoveArgument" />
    public class MoveTaskArgument : BaseEntity
    {
        [JsonConstructor]
        internal MoveTaskArgument()
        {
        }

        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        [JsonPropertyName("project_id")]
        public ComplexId? ProjectId { get; internal set; }

        /// <summary>
        /// Gets the section identifier.
        /// </summary>
        [JsonPropertyName("section_id")]
        public ComplexId? SectionId { get; internal set; }

        /// <summary>
        /// Gets the parent entity identifier.
        /// </summary>
        [JsonPropertyName("parent_id")]
        public ComplexId? ParentId { get; internal set; }

        /// <summary>
        /// Creates the move to project argument.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <param name="parentTaskId">Id of the destination parent task. The task becomes the last child task of the parent task.</param>
        /// <returns>
        /// Instance of <see cref="MoveTaskArgument" />
        /// </returns>
        public static MoveTaskArgument CreateMoveToParent(ComplexId taskId, ComplexId parentTaskId)
        {
            return new MoveTaskArgument { Id = taskId, ParentId = parentTaskId };
        }

        /// <summary>
        /// Creates the move to project argument.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <param name="projectId">Id of the destination project. The task becomes the last root task of the project.</param>
        /// <returns>Instance of <see cref="MoveTaskArgument" /></returns>
        public static MoveTaskArgument CreateMoveToProject(ComplexId taskId, ComplexId projectId)
        {
            return new MoveTaskArgument { Id = taskId, ProjectId = projectId };
        }

        /// <summary>
        /// Creates the move to project argument.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <param name="sectionId">Id of the destination section. The task becomes the last root task of the section.</param>
        /// <returns>Instance of <see cref="MoveTaskArgument" /></returns>
        public static MoveTaskArgument CreateMoveToSection(ComplexId taskId, ComplexId sectionId)
        {
            return new MoveTaskArgument { Id = taskId, SectionId = sectionId };
        }
    }
}
