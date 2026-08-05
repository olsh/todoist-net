using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the plan limits for a Todoist user.
    /// </summary>
    public class PlanLimits
    {
        /// <summary>
        /// Gets a value indicating whether the user has access to the activity log.
        /// </summary>
        [JsonPropertyName("activity_log")]
        public bool ActivityLog { get; internal set; }

        /// <summary>
        /// Gets the activity log limit. (-1 if there is no limit)
        /// </summary>
        [JsonPropertyName("activity_log_limit")]
        public int ActivityLogLimit { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has access to advanced permissions.
        /// </summary>
        [JsonPropertyName("advanced_permissions")]
        public bool AdvancedPermissions { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has access to automatic backups.
        /// </summary>
        [JsonPropertyName("automatic_backups")]
        public bool AutomaticBackups { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has access to calendar feeds.
        /// </summary>
        [JsonPropertyName("calendar_feeds")]
        public bool CalendarFeeds { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has access to calendar layout.
        /// </summary>
        [JsonPropertyName("calendar_layout")]
        public bool CalendarLayout { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has access to comments.
        /// </summary>
        [JsonPropertyName("comments")]
        public bool Comments { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has access to completed tasks.
        /// </summary>
        [JsonPropertyName("completed_tasks")]
        public bool CompletedTasks { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user can customize app icon.
        /// </summary>
        [JsonPropertyName("custom_app_icon")]
        public bool CustomAppIcon { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user can customize colors.
        /// </summary>
        [JsonPropertyName("customization_color")]
        public bool CustomizationColor { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has access to deadlines.
        /// </summary>
        [JsonPropertyName("deadlines")]
        public bool Deadlines { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has access to durations.
        /// </summary>
        [JsonPropertyName("durations")]
        public bool Durations { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has access to email AI forwarding.
        /// </summary>
        [JsonPropertyName("email_ai_forwarding")]
        public bool EmailAiForwarding { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has access to email forwarding.
        /// </summary>
        [JsonPropertyName("email_forwarding")]
        public bool EmailForwarding { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has access to filters.
        /// </summary>
        [JsonPropertyName("filters")]
        public bool Filters { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has access to labels.
        /// </summary>
        [JsonPropertyName("labels")]
        public bool Labels { get; internal set; }

        /// <summary>
        /// Gets the maximum number of calendar accounts.
        /// </summary>
        [JsonPropertyName("max_calendar_accounts")]
        public int MaxCalendarAccounts { get; internal set; }

        /// <summary>
        /// Gets the maximum number of collaborators.
        /// </summary>
        [JsonPropertyName("max_collaborators")]
        public int MaxCollaborators { get; internal set; }

        /// <summary>
        /// Gets the maximum number of filters.
        /// </summary>
        [JsonPropertyName("max_filters")]
        public int MaxFilters { get; internal set; }

        /// <summary>
        /// Gets the maximum number of folders per workspace.
        /// </summary>
        [JsonPropertyName("max_folders_per_workspace")]
        public int MaxFoldersPerWorkspace { get; internal set; }

        /// <summary>
        /// Gets the maximum number of free workspaces that can be created.
        /// </summary>
        [JsonPropertyName("max_free_workspaces_created")]
        public int MaxFreeWorkspacesCreated { get; internal set; }

        /// <summary>
        /// Gets the maximum number of guests per workspace.
        /// </summary>
        [JsonPropertyName("max_guests_per_workspace")]
        public int MaxGuestsPerWorkspace { get; internal set; }

        /// <summary>
        /// Gets the maximum number of labels.
        /// </summary>
        [JsonPropertyName("max_labels")]
        public int MaxLabels { get; internal set; }

        /// <summary>
        /// Gets the maximum number of live notifications.
        /// </summary>
        [JsonPropertyName("max_live_notifications")]
        public int MaxLiveNotifications { get; internal set; }

        /// <summary>
        /// Gets the maximum monthly ramble limit.
        /// </summary>
        [JsonPropertyName("max_monthly_ramble")]
        public int MaxMonthlyRamble { get; internal set; }

        /// <summary>
        /// Gets the maximum number of projects.
        /// </summary>
        [JsonPropertyName("max_projects")]
        public int MaxProjects { get; internal set; }

        /// <summary>
        /// Gets the maximum number of projects that can be joined.
        /// </summary>
        [JsonPropertyName("max_projects_joined")]
        public int MaxProjectsJoined { get; internal set; }

        /// <summary>
        /// Gets the maximum number of location reminders.
        /// </summary>
        [JsonPropertyName("max_reminders_location")]
        public int MaxRemindersLocation { get; internal set; }

        /// <summary>
        /// Gets the maximum number of time reminders.
        /// </summary>
        [JsonPropertyName("max_reminders_time")]
        public int MaxRemindersTime { get; internal set; }

        /// <summary>
        /// Gets the maximum number of sections.
        /// </summary>
        [JsonPropertyName("max_sections")]
        public int MaxSections { get; internal set; }

        /// <summary>
        /// Gets the maximum number of tasks.
        /// </summary>
        [JsonPropertyName("max_tasks")]
        public int MaxTasks { get; internal set; }

        /// <summary>
        /// Gets the maximum number of user templates.
        /// </summary>
        [JsonPropertyName("max_user_templates")]
        public int MaxUserTemplates { get; internal set; }

        /// <summary>
        /// Gets the plan name.
        /// </summary>
        [JsonPropertyName("plan_name")]
        public string PlanName { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has access to reminders.
        /// </summary>
        [JsonPropertyName("reminders")]
        public bool Reminders { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user can set reminders at due time.
        /// </summary>
        [JsonPropertyName("reminders_at_due")]
        public bool RemindersAtDue { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has access to templates.
        /// </summary>
        [JsonPropertyName("templates")]
        public bool Templates { get; internal set; }

        /// <summary>
        /// Gets the upload limit in megabytes.
        /// </summary>
        [JsonPropertyName("upload_limit_mb")]
        public int UploadLimitMb { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has access to uploads.
        /// </summary>
        [JsonPropertyName("uploads")]
        public bool Uploads { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has access to weekly trends.
        /// </summary>
        [JsonPropertyName("weekly_trends")]
        public bool WeeklyTrends { get; internal set; }
    }
}
