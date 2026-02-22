using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a set of workspace limit and feature values.
    /// </summary>
    public class WorkspaceLimitSet
    {
        /// <summary>
        /// Gets a value indicating whether admin tools are available.
        /// </summary>
        [JsonPropertyName("admin_tools")]
        public bool AdminTools { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether advanced permissions are available.
        /// </summary>
        [JsonPropertyName("advanced_permissions")]
        public bool AdvancedPermissions { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether automatic backups are available.
        /// </summary>
        [JsonPropertyName("automatic_backups")]
        public bool AutomaticBackups { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether calendar layout is available.
        /// </summary>
        [JsonPropertyName("calendar_layout")]
        public bool CalendarLayout { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether durations are available.
        /// </summary>
        [JsonPropertyName("durations")]
        public bool Durations { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether AI email forwarding is available.
        /// </summary>
        [JsonPropertyName("email_ai_forwarding")]
        public bool EmailAiForwarding { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether email forwarding is available.
        /// </summary>
        [JsonPropertyName("email_forwarding")]
        public bool EmailForwarding { get; internal set; }

        /// <summary>
        /// Gets the maximum collaborators.
        /// </summary>
        [JsonPropertyName("max_collaborators")]
        public int MaxCollaborators { get; internal set; }

        /// <summary>
        /// Gets the maximum filters.
        /// </summary>
        [JsonPropertyName("max_filters")]
        public int MaxFilters { get; internal set; }

        /// <summary>
        /// Gets the maximum folders per workspace.
        /// </summary>
        [JsonPropertyName("max_folders_per_workspace")]
        public int MaxFoldersPerWorkspace { get; internal set; }

        /// <summary>
        /// Gets the maximum guests per workspace.
        /// </summary>
        [JsonPropertyName("max_guests_per_workspace")]
        public int MaxGuestsPerWorkspace { get; internal set; }

        /// <summary>
        /// Gets the maximum live notifications.
        /// </summary>
        [JsonPropertyName("max_live_notifications")]
        public int MaxLiveNotifications { get; internal set; }

        /// <summary>
        /// Gets the maximum monthly ramble.
        /// </summary>
        [JsonPropertyName("max_monthly_ramble")]
        public int MaxMonthlyRamble { get; internal set; }

        /// <summary>
        /// Gets the maximum projects.
        /// </summary>
        [JsonPropertyName("max_projects")]
        public int MaxProjects { get; internal set; }

        /// <summary>
        /// Gets the maximum workspace templates.
        /// </summary>
        [JsonPropertyName("max_workspace_templates")]
        public int MaxWorkspaceTemplates { get; internal set; }

        /// <summary>
        /// Gets the maximum workspace users.
        /// </summary>
        [JsonPropertyName("max_workspace_users")]
        public int MaxWorkspaceUsers { get; internal set; }

        /// <summary>
        /// Gets the maximum workspaces.
        /// </summary>
        [JsonPropertyName("max_workspaces")]
        public int MaxWorkspaces { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether milestones are available.
        /// </summary>
        [JsonPropertyName("milestones")]
        public bool Milestones { get; internal set; }

        /// <summary>
        /// Gets the plan name.
        /// </summary>
        [JsonPropertyName("plan_name")]
        public string PlanName { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether project insights are available.
        /// </summary>
        [JsonPropertyName("project_insights")]
        public bool ProjectInsights { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether reminders are available.
        /// </summary>
        [JsonPropertyName("reminders")]
        public bool Reminders { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether due-time reminders are available.
        /// </summary>
        [JsonPropertyName("reminders_at_due")]
        public bool RemindersAtDue { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether security controls are available.
        /// </summary>
        [JsonPropertyName("security_controls")]
        public bool SecurityControls { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether team activity is available.
        /// </summary>
        [JsonPropertyName("team_activity")]
        public bool TeamActivity { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether extended team activity is available.
        /// </summary>
        [JsonPropertyName("team_activity_plus")]
        public bool TeamActivityPlus { get; internal set; }

        /// <summary>
        /// Gets the upload limit in megabytes.
        /// </summary>
        [JsonPropertyName("upload_limit_mb")]
        public int UploadLimitMb { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether workspace goals are available.
        /// </summary>
        [JsonPropertyName("workspace_goals")]
        public bool WorkspaceGoals { get; internal set; }
    }
}
