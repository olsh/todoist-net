using System;
using System.Globalization;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents due dates for tasks and reminders.
    /// </summary>
    public class DueDate
    {
        private const string FullDayEventDateFormat = "yyyy-MM-dd";

        /// <summary>
        /// Initializes a new instance of the <see cref="DueDate" /> class.
        /// </summary>
        /// <param name="date">The date time.</param>
        /// <param name="isFullDay">if set to <c>true</c> then it's a full day event.</param>
        /// <param name="timezone">The timezone.</param>
        public DueDate(DateTime date, bool isFullDay = false, string timezone = null)
        {
            Date = date;
            IsFullDay = isFullDay;
            Timezone = timezone;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DueDate" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="timezone">The timezone.</param>
        /// <param name="language">The language.</param>
        public DueDate(string text, string timezone = null, Language language = null)
        {
            Text = text;
            Timezone = timezone;
            Language = language;
        }

        internal DueDate()
        {
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        [JsonIgnore]
        public DateTime? Date { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is full day.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is full day; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool IsFullDay { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is recurring.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is recurring; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("is_recurring")]
        public bool? IsRecurring { get; internal set; }

        /// <summary>
        /// Gets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        [JsonProperty("lang")]
        public Language Language { get; internal set; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [JsonProperty("string")]
        public string Text { get; internal set; }

        /// <summary>
        /// Gets the timezone.
        /// </summary>
        /// <value>
        /// The timezone.
        /// </value>
        [JsonProperty("timezone")]
        public string Timezone { get; internal set; }

        /// <summary>
        /// Gets or sets the string date.
        /// </summary>
        /// <value>
        /// The string date.
        /// </value>
        /// <remarks>
        /// Format date according this rules https://developer.todoist.com/sync/v8/?shell#due-dates
        /// </remarks>
        [JsonProperty("date")]
        internal string StringDate
        {
            get
            {
                if (Date == null)
                {
                    return null;
                }

                var date = Date.Value.ToUniversalTime();
                if (IsFullDay)
                {
                    return date.ToString(FullDayEventDateFormat);
                }

                if (string.IsNullOrEmpty(Timezone))
                {
                    return date.ToString("s");
                }

                return date.ToString("s") + "Z";
            }

            set
            {
                if (DateTime.TryParseExact(value, FullDayEventDateFormat, null, DateTimeStyles.None, out var date))
                {
                    IsFullDay = true;
                    Date = date;

                    return;
                }

                Date = DateTime.Parse(value);
            }
        }
    }
}
