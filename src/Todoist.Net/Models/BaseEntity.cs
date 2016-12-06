using Newtonsoft.Json;

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
        [JsonProperty("id")]
        public ComplexId Id { get; set; }

        internal bool ShouldSerializeId()
        {
            return !Id.IsEmpty;
        }
    }
}
