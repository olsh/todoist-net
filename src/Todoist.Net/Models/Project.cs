using Newtonsoft.Json;

using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist project.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.BaseEntity" />
    public class Project : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Project(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Project"/> is collapsed.
        /// </summary>
        /// <value><c>null</c> if [collapsed] contains no value, <c>true</c> if [collapsed]; otherwise, <c>false</c>.</value>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("collapsed")]
        public bool? Collapsed { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        [JsonProperty("color")]
        public int? Color { get; set; }

        /// <summary>
        /// Gets or sets the indent.
        /// </summary>
        /// <value>The indent.</value>
        [JsonProperty("indent")]
        public int? Indent { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is archived.
        /// </summary>
        /// <value><c>true</c> if this instance is archived; otherwise, <c>false</c>.</value>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("is_archived")]
        public bool IsArchived { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; internal set; }

        /// <summary>
        /// Gets or sets the item order.
        /// </summary>
        /// <value>The item order.</value>
        [JsonProperty("item_order")]
        public int? ItemOrder { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Project"/> is shared.
        /// </summary>
        /// <value><c>true</c> if shared; otherwise, <c>false</c>.</value>
        [JsonProperty("shared")]
        public bool Shared { get; internal set; }
    }
}
