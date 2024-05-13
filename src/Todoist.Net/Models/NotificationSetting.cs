using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a notification setting.
    /// </summary>
    public class NotificationSetting
    {
        /// <summary>
        /// Gets a value indicating whether [notify email].
        /// </summary>
        /// <value><c>true</c> if [notify email]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("notify_email")]
        public bool NotifyEmail { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether [notify push].
        /// </summary>
        /// <value><c>true</c> if [notify push]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("notify_push")]
        public bool NotifyPush { get; internal set; }
    }
}
