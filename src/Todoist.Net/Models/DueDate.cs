using System;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents due dates for tasks and reminders.
    /// </summary>
    public class DueDate
    {
        private const string FullDayEventDateFormat = "yyyy-MM-dd";
        private const string DefaultEventDateFormat = "yyyy-MM-ddTHH:mm:ssK";

        [JsonConstructor]
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
        [JsonPropertyName("is_recurring")]
        public bool? IsRecurring { get; internal set; }

        /// <summary>
        /// Gets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        [JsonPropertyName("lang")]
        public Language Language { get; internal set; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [JsonPropertyName("string")]
        public string Text { get; internal set; }

        /// <summary>
        /// Gets the timezone.
        /// </summary>
        /// <value>
        /// The timezone.
        /// </value>
        [JsonPropertyName("timezone")]
        public string Timezone { get; internal set; }

        /// <summary>
        /// Gets or sets the string date.
        /// </summary>
        /// <value>
        /// The string date.
        /// </value>
        /// <remarks>
        /// Format date according this rules https://developer.todoist.com/sync/v9/?shell#due-dates
        /// </remarks>
        [JsonInclude]
        [JsonPropertyName("date")]
        internal string StringDate
        {
            get
            {
                if (Date == null)
                {
                    return null;
                }

                if (IsFullDay)
                {
                    return Date.Value.ToString(FullDayEventDateFormat);
                }

                return Date.Value.ToString(DefaultEventDateFormat);
            }

            set
            {
                if (DateTime.TryParseExact(value, FullDayEventDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    IsFullDay = true;
                    Date = date;

                    return;
                }

                Date = DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }
        }


        /// <summary>
        /// Creates a new instance of <see cref="DueDate"/> from text (every day; each Monday).
        /// </summary>
        /// <remarks>
        /// See <see href="https://developer.todoist.com/sync/v9/#due-dates">Todoist documentation</see> for more information.
        /// </remarks>
        /// <param name="text">The human-readable representation of due date (every day; each Monday)</param>
        /// <param name="language">The language of the text.</param>
        /// <returns>The created <see cref="DueDate"/> instance.</returns>
        public static DueDate FromText(string text, Language language = null) => new DueDate
        {
            Text = text,
            Language = language
        };

        /// <summary>
        /// Creates a full-day <see cref="DueDate"/>.
        /// </summary>
        /// <param name="date">The full-day date.</param>
        /// <remarks>
        /// <para>
        /// Only the date component of the given <see cref="DateTime"/> is used, and any time data is truncated.
        /// </para>
        /// <para>
        /// See <see href="https://developer.todoist.com/sync/v9/#due-dates">Todoist documentation</see> for more information.
        /// </para>
        /// </remarks>
        /// <returns>The created <see cref="DueDate"/> instance.</returns>
        public static DueDate CreateFullDay(DateTime date) => new DueDate
        {
            Date = date.Date,
            IsFullDay = true
        };

        /// <summary>
        /// Creates a floating <see cref="DueDate"/>.
        /// </summary>
        /// <param name="dateTime">The floating date.</param>
        /// <remarks>
        /// <para>
        /// The given <see cref="DateTime"/> is treated as <see cref="DateTimeKind.Unspecified"/>, and any timezone data is truncated.
        /// </para>
        /// <para>
        /// Floating due dates always represent an event in the current user's timezone.
        /// <br/>
        /// Note that it's not quite compatible with <see href="https://datatracker.ietf.org/doc/html/rfc3339">RFC 3339</see>,
        /// because the concept of timezone is not applicable to this object.
        /// </para>
        /// <para>
        /// See <see href="https://developer.todoist.com/sync/v9/#due-dates">Todoist documentation</see> for more information.
        /// </para>
        /// </remarks>
        /// <returns>The created <see cref="DueDate"/> instance.</returns>
        public static DueDate CreateFloating(DateTime dateTime) => new DueDate
        {
            Date = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified), // Floating dates are unspecified.
            IsFullDay = false
        };

        /// <summary>
        /// Creates a fixed timezone <see cref="DueDate"/>.
        /// </summary>
        /// <param name="dateTime">The fixed timezone date.</param>
        /// <param name="timezone">The timezone of the due instance.</param>
        /// <remarks>
        /// <para>
        /// Fixed due date is stored in UTC, hence, <see cref="DateTime"/> of kind <see cref="DateTimeKind.Unspecified"/> is assumed UTC,
        /// <br/>and <see cref="DateTime"/> of kind <see cref="DateTimeKind.Local"/> is converted to UTC based on the system timezone.
        /// </para>
        /// <para>
        /// See <see href="https://developer.todoist.com/sync/v9/#due-dates">Todoist documentation</see> for more information.
        /// </para>
        /// </remarks>
        /// <returns>The created <see cref="DueDate"/> instance.</returns>
        public static DueDate CreateFixedTimeZone(DateTime dateTime, string timezone)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc); // Unspecified dates are assumed UTC.
            }
            return new DueDate
            {
                Date = dateTime.ToUniversalTime(), // Local dates are converted to UTC.
                IsFullDay = false,
                Timezone = timezone
            };
        }

    }
}
