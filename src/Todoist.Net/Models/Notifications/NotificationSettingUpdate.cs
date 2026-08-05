using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a notification setting update payload.
    /// </summary>
    public class NotificationSettingUpdate
    {
        /// <summary>
        /// Gets or sets the notification type.
        /// </summary>
        [JsonPropertyName("notification_type")]
        public NotificationType NotificationType { get; set; }

        /// <summary>
        /// Gets or sets the notification channel service.
        /// </summary>
        [JsonPropertyName("service")]
        public NotificationService Service { get; set; }

        /// <summary>
        /// Gets or sets the optional token.
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether notifications should be suppressed.
        /// </summary>
        [JsonPropertyName("dont_notify")]
        public bool? DontNotify { get; set; }
    }
}
