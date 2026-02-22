using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents user settings update command payload.
    /// </summary>
    public class UpdateUserSettings : ICommandArgument
    {
        /// <summary>
        /// Gets or sets a value indicating whether push reminders are enabled.
        /// </summary>
        [JsonPropertyName("reminder_push")]
        public bool? ReminderPush { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether desktop reminders are enabled.
        /// </summary>
        [JsonPropertyName("reminder_desktop")]
        public bool? ReminderDesktop { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether email reminders are enabled.
        /// </summary>
        [JsonPropertyName("reminder_email")]
        public bool? ReminderEmail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether desktop completed sound is enabled.
        /// </summary>
        [JsonPropertyName("completed_sound_desktop")]
        public bool? CompletedSoundDesktop { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether mobile completed sound is enabled.
        /// </summary>
        [JsonPropertyName("completed_sound_mobile")]
        public bool? CompletedSoundMobile { get; set; }
    }
}
