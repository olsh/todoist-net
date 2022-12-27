using System;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a move argument.
    /// </summary>
    public class MoveArgument : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoveArgument" /> class.
        /// </summary>
        /// <param name="id">The identifier of moved entity.</param>
        /// <param name="parentId">The parent entity identifier.</param>
        /// <exception cref="T:System.ArgumentException">Entity ID is required for the operation</exception>
        public MoveArgument(ComplexId id, ComplexId parentId)
            : base(id)
        {
            if (id.IsEmpty)
            {
                throw new ArgumentException("Entity ID is required for the move operation", nameof(id));
            }

            if (parentId.IsEmpty)
            {
                throw new ArgumentException("Parent ID is required for the move operation", nameof(parentId));
            }

            ParentId = parentId;
        }

        internal MoveArgument()
        {
        }

        /// <summary>Gets the parent entity identifier.</summary>
        /// <value>The parent entity identifier.</value>
        [JsonProperty("parent_id")]
        public ComplexId ParentId { get; internal set; }
    }
}
