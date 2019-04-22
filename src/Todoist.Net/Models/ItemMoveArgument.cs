﻿using Newtonsoft.Json;

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
        /// Creates the move to project argument.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="parentItemId">The parent item identifier.</param>
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
        /// <param name="projectId">The project identifier.</param>
        /// <returns>Instance of <see cref="ItemMoveArgument" /></returns>
        public static ItemMoveArgument CreateMoveToProject(ComplexId itemId, ComplexId projectId)
        {
            return new ItemMoveArgument { Id = itemId, ProjectId = projectId };
        }
    }
}
