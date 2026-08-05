using System.Text.Json.Serialization;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a section-specific reorder entry.
    /// </summary>
    public class SectionReorderArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SectionReorderArgument" /> class.
        /// </summary>
        /// <param name="id">The section identifier.</param>
        /// <param name="sectionOrder">The new section order.</param>
        /// <exception cref="System.ArgumentException">Entity ID is required for reorder operation</exception>
        public SectionReorderArgument(ComplexId id, int sectionOrder)
        {
            ThrowHelper.ThrowIfNullOrEmpty(id.ToString(), nameof(id));

            Id = id;
            SectionOrder = sectionOrder;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public ComplexId Id { get; }

        /// <summary>
        /// Gets the new section order.
        /// </summary>
        [JsonPropertyName("section_order")]
        public int SectionOrder { get; }
    }
}