using System;

using Newtonsoft.Json;

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
        /// <exception cref="System.ArgumentException">
        /// Value cannot be null or empty. - name
        /// or
        /// Value cannot be null or empty. - query
        /// </exception>
        public Filter(string name, string query)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(query));
            }

            Name = name;
            Query = query;
        }

        internal Filter()
        {
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is favorite.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is favorite; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("is_favorite")]
        public bool IsFavorite { get; internal set; }

        /// <summary>
        /// Gets or sets the item order.
        /// </summary>
        /// <value>
        /// The item order.
        /// </value>
        /// <remarks>Filter’s order in the filter list (where the smallest value should place the filter at the top).</remarks>
        [JsonProperty("item_order")]
        public int ItemOrder { get; set; }

        /// <summary>
        /// Gets or sets the name of the filter.
        /// </summary>
        /// <value>
        /// The name of the filter.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the query to search for.
        /// </summary>
        /// <value>
        /// The query to search for.
        /// </value>
        [JsonProperty("query")]
        public string Query { get; set; }
    }
}
