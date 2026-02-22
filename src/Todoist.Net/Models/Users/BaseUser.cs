using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist user.
    /// </summary>
    public class BaseUser
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the weekend start day.
        /// </summary>
        [JsonPropertyName("weekend_start_day")]
        public DayOfWeek? WeekendStartDay { get; set; }

        /// <summary>
        /// Gets or sets the start day.
        /// </summary>
        [JsonPropertyName("start_day")]
        public DayOfWeek? StartDay { get; set; }

        /// <summary>
        /// Gets or sets the next week.
        /// </summary>
        [JsonPropertyName("next_week")]
        public DayOfWeek? NextWeek { get; set; }

        /// <summary>
        /// Gets or sets the time format.
        /// </summary>
        [JsonPropertyName("time_format")]
        public TimeFormat? TimeFormat { get; set; }

        /// <summary>
        /// Gets or sets the date format.
        /// </summary>
        [JsonPropertyName("date_format")]
        public DateFormat? DateFormat { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        [JsonPropertyName("sort_order")]
        public OrderType? SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the automatic reminder.
        /// </summary>
        [JsonPropertyName("auto_reminder")]
        public long? AutoReminder { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        [JsonPropertyName("lang")]
        public Language Language { get; set; }

        /// <summary>
        /// Gets or sets the user's default view on Todoist.
        /// </summary>
        /// <remarks>
        /// The start page can be one of the following: 
        /// <list type="bullet">
        /// <item><description><c>inbox</c></description></item>
        /// <item><description><c>teaminbox</c></description></item>
        /// <item><description><c>today</c></description></item>
        /// <item><description><c>next7days</c></description></item>
        /// <item><description><c>project?id=1234</c> to open a project</description></item>
        /// <item><description><c>label?name=abc</c> to open a label</description></item>
        /// <item><description><c>filter?id=1234</c> to open a personal filter</description></item>
        /// <item><description><c>workspace_filter?id=1234</c> to open a workspace filter</description></item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("start_page")]
        public string StartPage { get; set; }
    }
}
