namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a reminder type.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.StringEnum" />
    public class ReminderType : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReminderType" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        private ReminderType(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Gets the absolute.
        /// </summary>
        /// <value>The absolute.</value>
        /// <remarks>For a time-based reminder with a specific time and date in the future.</remarks>
        public static ReminderType Absolute { get; } = new ReminderType("absolute");

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>The location.</value>
        /// <remarks>For a location-based reminder.</remarks>
        public static ReminderType Location { get; } = new ReminderType("location");

        /// <summary>
        /// Gets the relative.
        /// </summary>
        /// <value>The relative.</value>
        /// <remarks>For a time-based reminder specified in minutes from now.</remarks>
        public static ReminderType Relative { get; } = new ReminderType("relative ");
    }
}
