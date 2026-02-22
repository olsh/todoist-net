using System.Text.Json.Serialization;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a move argument.
    /// </summary>
    public class MoveArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoveArgument" /> class.
        /// </summary>
        /// <param name="id">The identifier of moved entity.</param>
        /// <param name="parentId">The parent entity identifier.</param>
        /// <exception cref="T:System.ArgumentException">Entity ID is required for the operation</exception>
        public MoveArgument(ComplexId id, ComplexId parentId)
        {
            ThrowHelper.ThrowIfNullOrEmpty(id.ToString(), nameof(id));
            ThrowHelper.ThrowIfNullOrEmpty(parentId.ToString(), nameof(parentId));

            Id = id;
            ParentId = parentId;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonPropertyName("id")]
        public ComplexId Id { get; }

        /// <summary>Gets the parent entity identifier.</summary>
        /// <value>The parent entity identifier.</value>
        [JsonPropertyName("parent_id")]
        public ComplexId ParentId { get; }
    }
}
