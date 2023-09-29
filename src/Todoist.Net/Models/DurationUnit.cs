namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a duration unit.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.StringEnum" />
    public class DurationUnit : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DurationUnit" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        private DurationUnit(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Gets the minute unit.
        /// </summary>
        /// <value>The minute unit.</value>
        public static DurationUnit Minute { get; } = new DurationUnit("minute");

        /// <summary>
        /// Gets the day unit.
        /// </summary>
        /// <value>The day unit.</value>
        public static DurationUnit Day { get; } = new DurationUnit("day");

    }
}
