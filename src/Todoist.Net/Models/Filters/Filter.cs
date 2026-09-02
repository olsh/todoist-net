using System.Text.Json.Serialization;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// The filter.
    /// </summary>
    /// <remarks>Filters are only available for Todoist Premium users.</remarks>
    /// <seealso cref="Todoist.Net.Models.BaseEntity" />
    public class Filter : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Filter" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="query">The query.</param>
        /// <param name="color">The color.</param>
        /// <param name="itemOrder">The item order.</param>
        /// <param name="isFavorite">Indicates whether the filter is a favorite.</param>
        public Filter(string name, string query, Color color = null, int? itemOrder = null, bool isFavorite = false)
        {
            ThrowHelper.ThrowIfNullOrEmpty(name, nameof(name));
            ThrowHelper.ThrowIfNullOrEmpty(query, nameof(query));

            Name = name;
            Query = query;
            Color = color;
            ItemOrder = itemOrder;
            IsFavorite = isFavorite;
        }

        [JsonConstructor]
        internal Filter()
        {
        }

        /// <summary>
        /// Gets or sets the name of the filter.
        /// </summary>
        /// <value>
        /// The name of the filter.
        /// </value>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the query to search for.
        /// </summary>
        /// <value>
        /// The query to search for.
        /// </value>
        [JsonPropertyName("query")]
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the item order.
        /// </summary>
        /// <value>
        /// The item order.
        /// </value>
        /// <remarks>Filter’s order in the filter list (where the smallest value should place the filter at the top).</remarks>
        [JsonPropertyName("item_order")]
        public int? ItemOrder { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        [JsonPropertyName("color")]
        public Color Color { get; set; }

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
        /// <value>
        /// <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is frozen.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is frozen; otherwise, <c>false</c>.
        /// </value>
        [JsonPropertyName("is_frozen")]
        public bool IsFrozen { get; internal set; }
    }
}
