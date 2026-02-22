using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a connected calendar.
    /// </summary>
    public class Calendar
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        /// <value>The ID.</value>
        [JsonPropertyName("id")]
        public string Id { get; internal set; }
        
        /// <summary>
        /// Gets the account ID.
        /// </summary>
        /// <value>The account ID.</value>
        [JsonPropertyName("account_id")]
        public string AccountId { get; internal set; }

        /// <summary>
        /// Gets the color.
        /// </summary>
        /// <value>The color.</value>
        [JsonPropertyName("color")]
        public string Color { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is a task calendar.
        /// </summary>
        /// <value><c>true</c> if this instance is a task calendar; otherwise, <c>false</c>.</value>
        [JsonPropertyName("is_task_calendar")]
        public bool IsTaskCalendar { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        [JsonPropertyName("is_visible")]
        public bool IsVisible { get; internal set; }

        /// <summary>
        /// Gets the summary.
        /// </summary>
        /// <value>The summary.</value>
        [JsonPropertyName("summary")]
        public string Summary { get; internal set; }
    }
}
