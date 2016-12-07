namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a reminder type.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.StringEnum" />
    public class NotificationService : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        private NotificationService(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        public static NotificationService Email { get; } = new NotificationService("email");

        /// <summary>
        /// Gets the push.
        /// </summary>
        /// <value>The push.</value>
        public static NotificationService Push { get; } = new NotificationService("push");
    }
}
