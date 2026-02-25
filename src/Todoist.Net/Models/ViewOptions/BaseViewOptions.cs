using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a base entity for view options.
    /// </summary>
    public class BaseViewOptions : ICommandArgument
    {
        /// <summary>
        /// Gets or sets the view mode.
        /// </summary>
        [JsonPropertyName("view_mode")]
        public ViewOptionsStyle ViewMode { get; set; }

        /// <summary>
        /// Gets or sets grouped by criteria.
        /// </summary>
        [JsonPropertyName("grouped_by")]
        public ViewOptionsGrouping GroupedBy { get; set; }

        /// <summary>
        /// Gets or sets sorted by criteria.
        /// </summary>
        [JsonPropertyName("sorted_by")]
        public ViewOptionsSorting SortedBy { get; set; }

        /// <summary>
        /// Gets or sets sort order.
        /// </summary>
        [JsonPropertyName("sort_order")]
        public SortingOrder SortOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether completed tasks should be shown.
        /// </summary>
        [JsonPropertyName("show_completed_tasks")]
        public bool? ShowCompletedTasks { get; set; }

        /// <summary>
        /// Gets or sets a JSON string with filter criteria.
        /// </summary>
        [JsonPropertyName("filtered_by")]
        public string FilteredBy { get; set; }

        /// <summary>
        /// Gets or sets calendar settings.
        /// </summary>
        [JsonPropertyName("calendar_settings")]
        public CalendarViewSettings CalendarSettings { get; set; }
    }
}
