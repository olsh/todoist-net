using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents user settings.
    /// </summary>
    public class UserSettings
    {
        /// <summary>
        /// Gets a value indicating whether AI email assist is enabled.
        /// </summary>
        [JsonPropertyName("ai_email_assist")]
        public bool AiEmailAssist { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether desktop completed sound is enabled.
        /// </summary>
        [JsonPropertyName("completed_sound_desktop")]
        public bool CompletedSoundDesktop { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether mobile completed sound is enabled.
        /// </summary>
        [JsonPropertyName("completed_sound_mobile")]
        public bool CompletedSoundMobile { get; internal set; }

        /// <summary>
        /// Gets the timestamp until debug logging is enabled.
        /// </summary>
        [JsonPropertyName("debug_logging_enabled_until")]
        public DateTime? DebugLoggingEnabledUntil { get; internal set; }

        /// <summary>
        /// Gets the habit push notifications settings.
        /// </summary>
        [JsonPropertyName("habit_push_notifications")]
        public HabitPushNotificationsSettings HabitPushNotifications { get; internal set; }

        /// <summary>
        /// Gets the legacy pricing value.
        /// </summary>
        [JsonPropertyName("legacy_pricing")]
        public int LegacyPricing { get; internal set; }

        /// <summary>
        /// Gets the navigation settings.
        /// </summary>
        [JsonPropertyName("navigation")]
        public NavigationSettings Navigation { get; internal set; }

        /// <summary>
        /// Gets the quick add settings.
        /// </summary>
        [JsonPropertyName("quick_add")]
        public QuickAddSettings QuickAdd { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether desktop reminders are enabled.
        /// </summary>
        [JsonPropertyName("reminder_desktop")]
        public bool ReminderDesktop { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether email reminders are enabled.
        /// </summary>
        [JsonPropertyName("reminder_email")]
        public bool ReminderEmail { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether push reminders are enabled.
        /// </summary>
        [JsonPropertyName("reminder_push")]
        public bool ReminderPush { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether recurring subtasks are reset automatically.
        /// </summary>
        [JsonPropertyName("reset_recurring_subtasks")]
        public bool ResetRecurringSubtasks { get; internal set; }
    }
}
