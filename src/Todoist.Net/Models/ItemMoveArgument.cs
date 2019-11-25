using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a item move argument.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.MoveArgument" />
    public class ItemMoveArgument : MoveArgument
    {
        internal ItemMoveArgument()
        {
        }

        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        [JsonProperty("project_id")]
        public ComplexId? ProjectId { get; internal set; }

        /// <summary>
        /// Gets the section identifier.
        /// </summary>
        /// <value>
        /// The section identifier.
        /// </value>
        [JsonProperty("section_id")]
        public ComplexId? SectionId { get; internal set; }

        /// <summary>
        /// Creates the move to project argument.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="parentItemId">Id of the destination parent task. The task becomes the last child task of the parent task.</param>
        /// <returns>
        /// Instance of <see cref="ItemMoveArgument" />
        /// </returns>
        public static ItemMoveArgument CreateMoveToParent(ComplexId itemId, ComplexId parentItemId)
        {
            return new ItemMoveArgument { Id = itemId, ParentId = parentItemId };
        }

        /// <summary>
        /// Creates the move to project argument.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="projectId">Id of the destination project. The task becomes the last root task of the project.</param>
        /// <returns>Instance of <see cref="ItemMoveArgument" /></returns>
        public static ItemMoveArgument CreateMoveToProject(ComplexId itemId, ComplexId projectId)
        {
            return new ItemMoveArgument { Id = itemId, ProjectId = projectId };
        }

        /// <summary>
        /// Creates the move to project argument.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="sectionId">Id of the destination section. The task becomes the last root task of the section.</param>
        /// <returns>Instance of <see cref="ItemMoveArgument" /></returns>
        public static ItemMoveArgument CreateMoveToSection(ComplexId itemId, ComplexId sectionId)
        {
            return new ItemMoveArgument { Id = itemId, SectionId = sectionId };
        }
    }
}
