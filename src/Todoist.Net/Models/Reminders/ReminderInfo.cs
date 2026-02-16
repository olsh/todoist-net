using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents an information about a Todoist reminder.
    /// </summary>
    public class ReminderInfo
    {
        /// <summary>
        /// Gets the reminder.
        /// </summary>
        /// <value>The reminder.</value>
        [JsonPropertyName("reminder")]
        public Reminder Reminder { get; internal set; }
    }
}
