namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a calendar type.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.StringEnum" />
    public class CalendarType : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarType" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        private CalendarType(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Gets the Google calendar type.
        /// </summary>
        /// <value>The Google calendar type.</value>
        public static CalendarType GoogleCalendar { get; } = new CalendarType("google_calendar");

        /// <summary>
        /// Gets the Outlook calendar type.
        /// </summary>
        /// <value>The Outlook calendar type.</value>
        public static CalendarType OutlookCalendar { get; } = new CalendarType("outlook_calendar");
    }
}
