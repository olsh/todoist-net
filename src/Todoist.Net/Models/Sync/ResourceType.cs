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

        /// <summary>Gets all resources.</summary>
        internal static ResourceType All { get; } = new ResourceType("all");

        /// <summary>Gets the workspaces.</summary>
        public static ResourceType Workspaces { get; } = new ResourceType("workspaces");

        /// <summary>Gets the workspace_users.</summary>
        /// <remarks>The sync API returns values for this type in incremental sync only.</remarks>
        public static ResourceType WorkspaceUsers { get; } = new ResourceType("workspace_users");

        /// <summary>Gets the workspace_filters.</summary>
        public static ResourceType WorkspaceFilters { get; } = new ResourceType("workspace_filters");

        /// <summary>Gets the projects.</summary>
        public static ResourceType Projects { get; } = new ResourceType("projects");

        /// <summary>Gets the comments.</summary>
        /// <remarks>The sync API value remains "notes" for backwards compatibility.</remarks>
        public static ResourceType Comments { get; } = new ResourceType("notes");

        /// <summary>Gets the sections.</summary>
        public static ResourceType Sections { get; } = new ResourceType("sections");

        /// <summary>Gets the tasks.</summary>
        /// <remarks>The sync API value remains "items" for backwards compatibility.</remarks>
        public static ResourceType Tasks { get; } = new ResourceType("items");

        /// <summary>Gets the day_orders.</summary>
        public static ResourceType DayOrders { get; } = new ResourceType("day_orders");

        /// <summary>Gets the labels.</summary>
        public static ResourceType Labels { get; } = new ResourceType("labels");

        /// <summary>Gets the filters.</summary>
        public static ResourceType Filters { get; } = new ResourceType("filters");

        /// <summary>Gets the reminders.</summary>
        public static ResourceType Reminders { get; } = new ResourceType("reminders");

        /// <summary>Gets the reminders_location.</summary>
        public static ResourceType RemindersLocation { get; } = new ResourceType("reminders_location");

        /// <summary>Gets the locations.</summary>
        public static ResourceType Locations { get; } = new ResourceType("locations");

        /// <summary>Gets the user.</summary>
        public static ResourceType User { get; } = new ResourceType("user");

        /// <summary>Gets the user_settings.</summary>
        public static ResourceType UserSettings { get; } = new ResourceType("user_settings");

        /// <summary>Gets the user_plan_limits.</summary>
        public static ResourceType UserPlanLimits { get; } = new ResourceType("user_plan_limits");

        /// <summary>Gets the project completed_info.</summary>
        public static ResourceType ProjectCompletedInfo { get; } = new ResourceType("completed_info");

        /// <summary>Gets the user stats.</summary>
        public static ResourceType UserStats { get; } = new ResourceType("stats");

        /// <summary>Gets the workspace folders.</summary>
        public static ResourceType WorkspaceFolders { get; } = new ResourceType("folders");
        
        /// <summary>Gets the view_options.</summary>
        public static ResourceType ViewOptions { get; } = new ResourceType("view_options");

        /// <summary>Gets the project_view_options_defaults.</summary>
        public static ResourceType ProjectViewOptionsDefaults { get; } = new ResourceType("project_view_options_defaults");

        /// <summary>Gets the collaborators.</summary>
        public static ResourceType Collaborators { get; } = new ResourceType("collaborators");

        /// <summary>Gets the role_actions.</summary>
        public static ResourceType RoleActions { get; } = new ResourceType("role_actions");

        /// <summary>Gets the live_notifications.</summary>
        public static ResourceType LiveNotifications { get; } = new ResourceType("live_notifications");

        /// <summary>Gets the notification_settings.</summary>
        public static ResourceType NotificationSettings { get; } = new ResourceType("notification_settings");

        /// <summary>Gets the calendar_accounts.</summary>
        public static ResourceType CalendarAccounts { get; } = new ResourceType("calendar_accounts");

        /// <summary>Gets the calendars.</summary>
        public static ResourceType Calendars { get; } = new ResourceType("calendars");
    }
}
