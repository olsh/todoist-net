namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a reminder type.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.StringEnum" />
    public class ReminderService : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReminderService" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        private ReminderService(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        public static ReminderService Email { get; } = new ReminderService("email");

        /// <summary>
        /// Gets the mobile.
        /// </summary>
        /// <value>The mobile.</value>
        public static ReminderService Mobile { get; } = new ReminderService("mobile");

        /// <summary>
        /// Gets the push.
        /// </summary>
        /// <value>The push.</value>
        public static ReminderService Push { get; } = new ReminderService("push");
    }
}
