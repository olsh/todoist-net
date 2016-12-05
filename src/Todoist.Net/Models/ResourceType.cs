namespace Todoist.Net.Models
{
    public class ResourceType
    {
        private ResourceType(string resource)
        {
            Resource = resource;
        }

        public static ResourceType All { get; } = new ResourceType("all");

        public static ResourceType Collaborators { get; } = new ResourceType("collaborators");

        public static ResourceType Filters { get; } = new ResourceType("filters");

        public static ResourceType Items { get; } = new ResourceType("items");

        public static ResourceType Labels { get; } = new ResourceType("labels");

        public static ResourceType LiveNotifications { get; } = new ResourceType("live_notifications");

        public static ResourceType Locations { get; } = new ResourceType("locations");

        public static ResourceType Notes { get; } = new ResourceType("notes");

        public static ResourceType NotificationsSettings { get; } = new ResourceType("notification_settings");

        public static ResourceType Projects { get; } = new ResourceType("projects");

        public static ResourceType Reminders { get; } = new ResourceType("reminders");

        public static ResourceType User { get; } = new ResourceType("user");

        public string Resource { get; }
    }
}
