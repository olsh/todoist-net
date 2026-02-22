using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents full workspace details returned by get requests.
    /// </summary>
    public class WorkspaceInfo : BaseWorkspace
    {
        /// <summary>
        /// Gets the workspace plan.
        /// </summary>
        [JsonPropertyName("plan")]
        public WorkspacePlan Plan { get; internal set; }

        /// <summary>
        /// Gets the invitation code.
        /// </summary>
        [JsonPropertyName("invite_code")]
        public string InviteCode { get; internal set; }

        /// <summary>
        /// Gets the requesting user role in the workspace.
        /// </summary>
        [JsonPropertyName("role")]
        public WorkspaceRole Role { get; internal set; }

        /// <summary>
        /// Gets the URL for the big workspace logo image.
        /// </summary>
        [JsonPropertyName("logo_big")]
        public string LogoBig { get; internal set; }

        /// <summary>
        /// Gets the URL for the medium workspace logo image.
        /// </summary>
        [JsonPropertyName("logo_medium")]
        public string LogoMedium { get; internal set; }

        /// <summary>
        /// Gets the URL for the small workspace logo image.
        /// </summary>
        [JsonPropertyName("logo_small")]
        public string LogoSmall { get; internal set; }

        /// <summary>
        /// Gets the URL for the square 640px workspace logo image.
        /// </summary>
        [JsonPropertyName("logo_s640")]
        public string LogoS640 { get; internal set; }

        /// <summary>
        /// Gets workspace feature limits for current and next plans.
        /// </summary>
        [JsonPropertyName("limits")]
        public WorkspaceLimits Limits { get; internal set; }

        /// <summary>
        /// Gets the id of the user who created the workspace.
        /// </summary>
        [JsonPropertyName("creator_id")]
        public string CreatorId { get; internal set; }

        /// <summary>
        /// Gets the date when the workspace was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the workspace is deleted.
        /// </summary>
        [JsonPropertyName("is_deleted")]
        public bool? IsDeleted { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the workspace is collapsed for the requesting user.
        /// </summary>
        [JsonPropertyName("is_collapsed")]
        public bool? IsCollapsed { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether a desktop workspace modal should be shown.
        /// </summary>
        [JsonPropertyName("desktop_workspace_modal")]
        public WorkspaceDesktopModal DesktopWorkspaceModal { get; internal set; }

        /// <summary>
        /// Gets the number of active projects in the workspace.
        /// </summary>
        [JsonPropertyName("current_active_projects")]
        public int? CurrentActiveProjects { get; internal set; }

        /// <summary>
        /// Gets the current number of members in the workspace.
        /// </summary>
        [JsonPropertyName("current_member_count")]
        public int? CurrentMemberCount { get; internal set; }

        /// <summary>
        /// Gets the current number of templates in the workspace.
        /// </summary>
        [JsonPropertyName("current_template_count")]
        public int? CurrentTemplateCount { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether a trial is pending.
        /// </summary>
        [JsonPropertyName("is_trial_pending")]
        public bool? IsTrialPending { get; internal set; }

        /// <summary>
        /// Gets member counts by role type.
        /// </summary>
        [JsonPropertyName("member_count_by_type")]
        public WorkspaceMemberCountByType MemberCountByType { get; internal set; }

        /// <summary>
        /// Gets pending invitation emails.
        /// </summary>
        [JsonPropertyName("pending_invitations")]
        public IReadOnlyCollection<string> PendingInvitations { get; internal set; }

        /// <summary>
        /// Gets pending invitation counts by role type.
        /// </summary>
        [JsonPropertyName("pending_invites_by_type")]
        public WorkspaceMemberCountByType PendingInvitesByType { get; internal set; }

        /// <summary>
        /// Gets or sets the project sort preference.
        /// </summary>
        [JsonPropertyName("project_sort_preference")]
        public WorkspaceSortPreference ProjectSortPreference { get; set; }

        /// <summary>
        /// Gets a value indicating whether user sorting is applied.
        /// </summary>
        [JsonPropertyName("user_sorting_applied")]
        public bool? UserSortingApplied { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether admin sorting is applied.
        /// </summary>
        [JsonPropertyName("admin_sorting_applied")]
        public bool? AdminSortingApplied { get; internal set; }
    }
}
