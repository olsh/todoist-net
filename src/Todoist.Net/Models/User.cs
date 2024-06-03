using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist user.
    /// </summary>
    public class User : ICommandArgument, IUnsettableProperties
    {
        HashSet<PropertyInfo> IUnsettableProperties.UnsetProperties { get; } = new HashSet<PropertyInfo>();


        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        [JsonPropertyName("lang")]
        public Language Language { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [JsonPropertyName("password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        /// <value>The time zone.</value>
        [JsonPropertyName("timezone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the automatic reminder.
        /// </summary>
        /// <value>The automatic reminder.</value>
        [JsonPropertyName("auto_reminder")]
        public long? AutoReminder { get; set; }

        /// <summary>
        /// The user's current password.
        /// This must be provided if the request is modifying the user's password or email address and the user already has a password set (indicated by has_password in the user object).
        /// For amending other properties this is not required.
        /// </summary>
        /// <value>
        /// The current password.
        /// </value>
        [JsonPropertyName("current_password")]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Gets or sets the date format.
        /// </summary>
        /// <value>The date format.</value>
        [JsonPropertyName("date_format")]
        public DateFormat? DateFormat { get; set; }

        /// <summary>
        /// Gets or sets the next week.
        /// </summary>
        /// <value>The next week.</value>
        [JsonPropertyName("next_week")]
        public DayOfWeek? NextWeek { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>The sort order.</value>
        [JsonPropertyName("sort_order")]
        public OrderType? SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the start day.
        /// </summary>
        /// <value>The start day.</value>
        [JsonPropertyName("start_day")]
        public DayOfWeek? StartDay { get; set; }

        /// <summary>
        /// Gets or sets the start page.
        /// </summary>
        /// <value>The start page.</value>
        [JsonPropertyName("start_page")]
        public string StartPage { get; set; }

        /// <summary>
        /// Gets or sets the theme.
        /// </summary>
        /// <value>The theme.</value>
        [JsonPropertyName("theme_id")]
        public string Theme { get; set; }

        /// <summary>
        /// Gets or sets the time format.
        /// </summary>
        /// <value>The time format.</value>
        [JsonPropertyName("time_format")]
        public TimeFormat? TimeFormat { get; set; }
    }
}
