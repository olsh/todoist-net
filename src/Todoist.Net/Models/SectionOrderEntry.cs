using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents an section order entry which can be used to specify order of a Todoist section.
    /// </summary>
    public class SectionOrderEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SectionOrderEntry" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="order">The order.</param>
        public SectionOrderEntry(ComplexId id, int order)
        {
            Id = id;
            Order = order;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonPropertyName("id")]
        public ComplexId Id { get; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        [JsonPropertyName("section_order")]
        public int Order { get; }
    }
}
