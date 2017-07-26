using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist user.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.UserBase" />
    public class User : UserBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="password">The password.</param>
        public User(string email, string fullName, string password)
            : base(email, fullName, password)
        {
        }

        internal User()
        {
        }

        /// <summary>
        /// Gets or sets the automatic reminder.
        /// </summary>
        /// <value>The automatic reminder.</value>
        [JsonProperty("auto_reminder")]
        public long? AutoReminder { get; set; }

        /// <summary>
        /// Gets or sets the date format.
        /// </summary>
        /// <value>The date format.</value>
        [JsonProperty("date_format")]
        public DateFormat? DateFormat { get; set; }

        /// <summary>
        /// Gets or sets the default reminder.
        /// </summary>
        /// <value>The default reminder.</value>
        [JsonProperty("default_reminder")]
        public ReminderService DefaultReminder { get; set; }

        /// <summary>
        /// Gets or sets the mobile host.
        /// </summary>
        /// <value>The mobile host.</value>
        [JsonProperty("mobile_host")]
        public string MobileHost { get; set; }

        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        /// <value>The mobile number.</value>
        [JsonProperty("mobile_number")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the next week.
        /// </summary>
        /// <value>The next week.</value>
        [JsonProperty("next_week")]
        public DayOfWeek? NextWeek { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>The sort order.</value>
        [JsonProperty("sort_order")]
        public OrderType? SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the start day.
        /// </summary>
        /// <value>The start day.</value>
        [JsonProperty("start_day")]
        public DayOfWeek? StartDay { get; set; }

        /// <summary>
        /// Gets or sets the start page.
        /// </summary>
        /// <value>The start page.</value>
        [JsonProperty("start_page")]
        public string StartPage { get; set; }

        /// <summary>
        /// Gets or sets the theme.
        /// </summary>
        /// <value>The theme.</value>
        [JsonProperty("theme")]
        public int? Theme { get; set; }

        /// <summary>
        /// Gets or sets the time format.
        /// </summary>
        /// <value>The time format.</value>
        [JsonProperty("time_format")]
        public TimeFormat? TimeFormat { get; set; }
    }
}
