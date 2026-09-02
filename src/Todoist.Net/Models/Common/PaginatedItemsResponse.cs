using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a paginated API response where items are returned in an <c>items</c> property.
    /// </summary>
    /// <typeparam name="T">The type of items in the response.</typeparam>
    public class PaginatedItemsResponse<T>
    {
        /// <summary>
        /// Gets or sets the items for the current page.
        /// </summary>
        /// <value>The items collection.</value>
        [JsonPropertyName("items")]
        public IReadOnlyList<T> Items { get; set; }

        /// <summary>
        /// Gets or sets the cursor for the next page, or <see langword="null"/> if no more pages.
        /// </summary>
        /// <value>The cursor string for the next page.</value>
        [JsonPropertyName("next_cursor")]
        public string NextCursor { get; set; }

        /// <summary>
        /// Gets a value indicating whether there are more pages available.
        /// </summary>
        /// <value><c>true</c> if there are more pages; otherwise, <c>false</c>.</value>
        public bool HasMore => !string.IsNullOrEmpty(NextCursor);
    }
}