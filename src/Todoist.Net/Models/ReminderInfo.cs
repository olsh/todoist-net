using Newtonsoft.Json;

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
        [JsonProperty("reminder")]
        public Reminder Reminder { get; internal set; }
    }
}
