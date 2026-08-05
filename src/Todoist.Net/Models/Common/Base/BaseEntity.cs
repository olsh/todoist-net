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

        [JsonConstructor]
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
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ComplexId Id { get; set; }
    }
}
