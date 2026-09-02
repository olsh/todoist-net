using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents habit push notifications settings.
    /// </summary>
    public class HabitPushNotificationsSettings
    {
        /// <summary>
        /// Gets the configured habit push notification features.
        /// </summary>
        [JsonPropertyName("features")]
        public IReadOnlyCollection<HabitPushNotificationFeature> Features { get; internal set; }
    }
    
    /// <summary>
    /// Represents a habit push notification feature.
    /// </summary>
    public class HabitPushNotificationFeature
    {
        /// <summary>
        /// Gets a value indicating whether the feature is enabled.
        /// </summary>
        [JsonPropertyName("enabled")]
        public bool Enabled { get; internal set; }

        /// <summary>
        /// Gets the feature name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the time when the notification should be sent.
        /// </summary>
        [JsonPropertyName("send_at")]
        public string SendAt { get; internal set; }
    }
}
