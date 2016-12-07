namespace Todoist.Net.Models
{
    /// <summary>
    /// Contains Todoist resource types.
    /// </summary>
    public class ResourceType : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceType"/> class.
        /// </summary>
        /// <param name="value">The resource.</param>
        private ResourceType(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Gets the filters.
        /// </summary>
        /// <value>The filters.</value>
        public static ResourceType Filters { get; } = new ResourceType("filters");

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        public static ResourceType Items { get; } = new ResourceType("items");

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <value>The labels.</value>
        public static ResourceType Labels { get; } = new ResourceType("labels");

        /// <summary>
        /// Gets the locations.
        /// </summary>
        /// <value>The locations.</value>
        public static ResourceType Locations { get; } = new ResourceType("locations");

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public static ResourceType Notes { get; } = new ResourceType("notes");

        /// <summary>
        /// Gets the live notifications.
        /// </summary>
        /// <value>The live notifications.</value>
        public static ResourceType Notifications { get; } = new ResourceType("live_notifications");

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>The projects.</value>
        public static ResourceType Projects { get; } = new ResourceType("projects");

        /// <summary>
        /// Gets the reminders.
        /// </summary>
        /// <value>The reminders.</value>
        public static ResourceType Reminders { get; } = new ResourceType("reminders");

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>The user.</value>
        public static ResourceType User { get; } = new ResourceType("user");

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <value>All resources.</value>
        internal static ResourceType All { get; } = new ResourceType("all");
    }
}
