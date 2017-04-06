namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a location trigger.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.StringEnum" />
    public class LocationTrigger : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationTrigger" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        private LocationTrigger(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Gets the on enter.
        /// </summary>
        /// <value>The on enter.</value>
        /// <remarks>For entering the location.</remarks>
        public static LocationTrigger OnEnter { get; } = new LocationTrigger("on_enter");

        /// <summary>
        /// Gets the on leave.
        /// </summary>
        /// <value>The on leave.</value>
        /// <remarks>For leaving the location.</remarks>
        public static LocationTrigger OnLeave { get; } = new LocationTrigger("on_leave");
    }
}
