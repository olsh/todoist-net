using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a connected calendar account.
    /// </summary>
    public class CalendarAccount
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        /// <value>The ID.</value>
        [JsonPropertyName("id")]
        public string Id { get; internal set; }

        /// <summary>
        /// Gets the calendars sync state.
        /// </summary>
        /// <value>The calendars sync state.</value>
        [JsonPropertyName("calendars_sync_state")]
        public string CalendarsSyncState { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether all-day tasks are enabled.
        /// </summary>
        /// <value><c>true</c> if all-day tasks are enabled; otherwise, <c>false</c>.</value>
        [JsonPropertyName("is_all_day_tasks_enabled")]
        public bool IsAllDayTasksEnabled { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether events are enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if events are enabled; otherwise, <c>false</c>.
        /// </value>
        [JsonPropertyName("is_events_enabled")]
        public bool IsEventsEnabled { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether tasks are enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if tasks are enabled; otherwise, <c>false</c>.
        /// </value>
        [JsonPropertyName("is_tasks_enabled")]
        public bool IsTasksEnabled { get; internal set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonPropertyName("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the pending operation until date.
        /// </summary>
        /// <value>The pending operation until date.</value>
        [JsonPropertyName("pending_operation_until")]
        public DateTime? PendingOperationUntil { get; internal set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        [JsonPropertyName("type")]
        public CalendarType Type { get; internal set; }
    }
}
