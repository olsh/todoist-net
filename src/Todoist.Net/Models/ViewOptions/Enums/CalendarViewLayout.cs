namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents calendar view layout values.
    /// </summary>
    public class CalendarViewLayout : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarViewLayout"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private CalendarViewLayout(string value) : base(value) { }

        /// <summary>Gets week view style.</summary>
        public static CalendarViewLayout Week { get; } = new CalendarViewLayout("WEEK");

        /// <summary>Gets month view style.</summary>
        public static CalendarViewLayout Month { get; } = new CalendarViewLayout("MONTH");
    }
}
