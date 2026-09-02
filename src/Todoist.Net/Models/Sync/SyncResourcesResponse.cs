using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the response of a Sync API call with resources.
    /// </summary>
    public class SyncResourcesResponse : BaseSyncResponse
    {
        /// <summary>Gets the workspaces.</summary>
        [JsonPropertyName("workspaces")]
        public IReadOnlyCollection<WorkspaceInfo> Workspaces { get; internal set; }
        
        /// <summary>Gets the workspace users.</summary>
        /// <remarks>The sync API returns data for this property in incremental sync only.</remarks>
        [JsonPropertyName("workspace_users")]
        public IReadOnlyCollection<WorkspaceUser> WorkspaceUsers { get; internal set; }
        
        /// <summary>Gets the workspace filters.</summary>
        [JsonPropertyName("workspace_filters")]
        public IReadOnlyCollection<WorkspaceFilterInfo> WorkspaceFilters { get; internal set; }
        
        /// <summary>Gets the projects.</summary>
        [JsonPropertyName("projects")]
        public IReadOnlyCollection<ProjectInfo> Projects { get; internal set; }

        /// <summary>Gets the comments.</summary>
        /// <remarks>The JSON property name remains "notes" for backwards compatibility with Sync API.</remarks>
        [JsonPropertyName("notes")]
        public IReadOnlyCollection<Comment> Comments { get; internal set; }

        /// <summary>Gets the project comments.</summary>
        /// <remarks>The JSON property name remains "project_notes" for backwards compatibility with Sync API.</remarks>
        [JsonPropertyName("project_notes")]
        public IReadOnlyCollection<Comment> ProjectComments { get; internal set; }

        /// <summary>Gets the incomplete task IDs.</summary>
        /// <remarks>The JSON property name remains "incomplete_item_ids" for backwards compatibility with Sync API.</remarks>
        [JsonPropertyName("incomplete_item_ids")]
        public IReadOnlyCollection<string> IncompleteTaskIds { get; internal set; }

        /// <summary>Gets the incomplete project IDs.</summary>
        [JsonPropertyName("incomplete_project_ids")]
        public IReadOnlyCollection<string> IncompleteProjectIds { get; internal set; }

        /// <summary>Gets the sections.</summary>
        [JsonPropertyName("sections")]
        public IReadOnlyCollection<Section> Sections { get; internal set; }

        /// <summary>Gets the tasks.</summary>
        /// <remarks>The JSON property name remains "items" for backwards compatibility with Sync API.</remarks>
        [JsonPropertyName("items")]
        public IReadOnlyCollection<TaskInfo> Tasks { get; internal set; }

        /// <summary>Gets the day orders.</summary>
        [JsonPropertyName("day_orders")]
        public Dictionary<string, int> DayOrders { get; internal set; }

        /// <summary>Gets the labels.</summary>
        [JsonPropertyName("labels")]
        public IReadOnlyCollection<Label> Labels { get; internal set; }

        /// <summary>Gets the filters.</summary>
        [JsonPropertyName("filters")]
        public IReadOnlyCollection<Filter> Filters { get; internal set; }

        /// <summary>Gets the reminders.</summary>
        [JsonPropertyName("reminders")]
        public IReadOnlyCollection<Reminder> Reminders { get; internal set; }

        /// <summary>Gets the reminders location.</summary>
        [JsonPropertyName("reminders_location")]
        public IReadOnlyCollection<Reminder> RemindersLocation { get; internal set; }

        /// <summary>Gets the locations.</summary>
        /// <remarks>The location object is specific, as it's not an object, but an ordered array</remarks>
        [JsonPropertyName("locations")]
        public IReadOnlyCollection<string[]> Locations { get; internal set; }

        /// <summary>Gets the folders.</summary>
        [JsonPropertyName("folders")]
        public IReadOnlyCollection<WorkspaceFolder> WorkspaceFolders { get; internal set; }

        /// <summary>Gets the completed tasks information.</summary>
        [JsonPropertyName("completed_info")]
        public IReadOnlyCollection<ProjectCompletedInfo> ProjectCompletedInfo { get; internal set; }

        /// <summary>Gets the view options.</summary>
        [JsonPropertyName("view_options")]
        public IReadOnlyCollection<ViewOptions> ViewOptions { get; internal set; }

        /// <summary>Gets the project view options defaults.</summary>
        [JsonPropertyName("project_view_options_defaults")]
        public IReadOnlyCollection<ProjectViewOptionsDefaults> ProjectViewOptionsDefaults { get; internal set; }

        /// <summary>Gets the collaborators.</summary>
        [JsonPropertyName("collaborators")]
        public IReadOnlyCollection<Collaborator> Collaborators { get; internal set; }

        /// <summary>Gets the collaborator states.</summary>
        [JsonPropertyName("collaborator_states")]
        public IReadOnlyCollection<CollaboratorState> CollaboratorStates { get; internal set; }

        /// <summary>Gets the calendars.</summary>
        [JsonPropertyName("calendars")]
        public IReadOnlyCollection<Calendar> Calendars { get; internal set; }

        /// <summary>Gets the calendar accounts.</summary>
        [JsonPropertyName("calendar_accounts")]
        public IReadOnlyCollection<CalendarAccount> CalendarAccounts { get; internal set; }

        /// <summary>Gets the notifications.</summary>
        [JsonPropertyName("live_notifications")]
        public IReadOnlyCollection<Notification> Notifications { get; internal set; }

        /// <summary>Gets or sets the last read notification identifier.</summary>
        [JsonPropertyName("live_notifications_last_read_id")]
        public long? LastReadNotificationId { get; set; }

        /// <summary>Gets the notification settings.</summary>
        /// <remarks>The JSON property name remains "settings_notifications" for backwards compatibility with Sync API.</remarks>
        [JsonPropertyName("settings_notifications")]
        public Dictionary<NotificationType, NotificationSetting> NotificationSettings { get; internal set; }

        /// <summary>Gets the user information.</summary>
        [JsonPropertyName("user")]
        public UserInfo UserInfo { get; internal set; }

        /// <summary>Gets the user settings.</summary>
        [JsonPropertyName("user_settings")]
        public UserSettings UserSettings { get; internal set; }

        /// <summary>Gets the user plan limits.</summary>
        [JsonPropertyName("user_plan_limits")]
        public UserPlanLimits UserPlanLimits { get; internal set; }

        /// <summary>Gets the user stats.</summary>
        [JsonPropertyName("stats")]
        public UserStats UserStats { get; internal set; }

        /// <summary>Gets the role actions.</summary>
        [JsonPropertyName("role_actions")]
        public ProjectPermissions ProjectRoleActions { get; internal set; }
    }
}
