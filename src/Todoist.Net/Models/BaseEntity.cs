using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a base entity.
    /// </summary>
    public class BaseEntity : ICommandArgument
    {
        internal BaseEntity(ComplexId id)
        {
            Id = id;
        }

        internal BaseEntity()
        {
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonPropertyName("id")]
        public ComplexId Id { get; set; }

        /// <summary>
        /// Checks if the Id property should be serialized.
        /// </summary>
        /// <returns><c>True</c> if the property should be serialized, <c>false</c> otherwise.</returns>
        public bool ShouldSerializeId()
        {
            return !Id.IsEmpty;
        }
    }
}
