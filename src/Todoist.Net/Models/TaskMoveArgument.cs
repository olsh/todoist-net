using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a task move argument.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.MoveArgument" />
    public class TaskMoveArgument : BaseEntity
    {
        [JsonConstructor]
        internal TaskMoveArgument()
        {
        }

        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        [JsonPropertyName("project_id")]
        public ComplexId? ProjectId { get; internal set; }

        /// <summary>
        /// Gets the section identifier.
        /// </summary>
        /// <value>
        /// The section identifier.
        /// </value>
        [JsonPropertyName("section_id")]
        public ComplexId? SectionId { get; internal set; }

        /// <summary>Gets the parent entity identifier.</summary>
        /// <value>The parent entity identifier.</value>
        [JsonPropertyName("parent_id")]
        public ComplexId? ParentId { get; internal set; }

        /// <summary>
        /// Creates the move to project argument.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <param name="parentTaskId">Id of the destination parent task. The task becomes the last child task of the parent task.</param>
        /// <returns>
        /// Instance of <see cref="TaskMoveArgument" />
        /// </returns>
        public static TaskMoveArgument CreateMoveToParent(ComplexId taskId, ComplexId parentTaskId)
        {
            return new TaskMoveArgument { Id = taskId, ParentId = parentTaskId };
        }

        /// <summary>
        /// Creates the move to project argument.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <param name="projectId">Id of the destination project. The task becomes the last root task of the project.</param>
        /// <returns>Instance of <see cref="TaskMoveArgument" /></returns>
        public static TaskMoveArgument CreateMoveToProject(ComplexId taskId, ComplexId projectId)
        {
            return new TaskMoveArgument { Id = taskId, ProjectId = projectId };
        }

        /// <summary>
        /// Creates the move to project argument.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <param name="sectionId">Id of the destination section. The task becomes the last root task of the section.</param>
        /// <returns>Instance of <see cref="TaskMoveArgument" /></returns>
        public static TaskMoveArgument CreateMoveToSection(ComplexId taskId, ComplexId sectionId)
        {
            return new TaskMoveArgument { Id = taskId, SectionId = sectionId };
        }
    }
}
