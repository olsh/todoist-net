using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a paginated API response for completed tasks.
    /// </summary>
    public class PaginatedCompletedTasks
    {
        /// <summary>
        /// Gets or sets the results for the current page.
        /// </summary>
        /// <value>The results collection.</value>
        [JsonPropertyName("items")]
        public IReadOnlyList<TaskInfo> Items { get; set; }

        /// <summary>
        /// Gets or sets the cursor for the next page, or null if no more pages.
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
