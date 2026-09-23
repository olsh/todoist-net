using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist section.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.BaseEntity" />
    public class UpdateSection : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSection" /> class.
        /// </summary>
        /// <param name="id">The section identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="isCollapsed">A value indicating whether the section’s tasks are collapsed.</param>
        public UpdateSection(ComplexId id, string name, bool? isCollapsed = null)
            : base(id)
        {
            Name = name;
            IsCollapsed = isCollapsed;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name of the section.
        /// </value>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is collapsed.
        /// </summary>
        /// <value>
        /// <c>true</c> if the section’s tasks are collapsed; otherwise, <c>false</c>.
        /// </value>
        [JsonPropertyName("is_collapsed")]
        public bool? IsCollapsed { get; set; }

        /// <summary>
        /// Gets or sets the fractional-indexing key which orders the section in its project.
        /// </summary>
        /// <remarks>
        /// Keys sort as ordinal strings. Without a key, the section keeps its current one.
        /// If another section already uses the key, Todoist stores a key right after it instead, which a sync returns.
        /// </remarks>
        /// <value>The order key.</value>
        [JsonPropertyName("order_key")]
        public string OrderKey { get; set; }
    }
}
