using System.Text.Json.Serialization;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist label.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.BaseEntity" />
    public class Label : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="color">The color.</param>
        /// <param name="itemOrder">The item order.</param>
        /// <param name="isFavorite">Indicates whether the label is a favorite.</param>
        public Label(string name, Color color = null, int? itemOrder = null, bool isFavorite = false)
        {
            ThrowHelper.ThrowIfNullOrEmpty(name, nameof(name));
            
            Name = name;
            Color = color;
            ItemOrder = itemOrder;
            IsFavorite = isFavorite;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        [JsonPropertyName("color")]
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the item order.
        /// </summary>
        /// <value>The item order.</value>
        [JsonPropertyName("item_order")]
        public int? ItemOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is favorite.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is favorite; otherwise, <c>false</c>.
        /// </value>
        [JsonPropertyName("is_favorite")]
        public bool IsFavorite { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; internal set; }
    }
}
