namespace Todoist.Net.Models
{
    /// <summary>
    /// Contains types of Todoist notifications.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.StringEnum" />
    public class NotificationType : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationType"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        private NotificationType(string value)
            : base(value)
        {
        }

        /// <summary>Gets the biz account disabled.</summary>
        public static NotificationType BizAccountDisabled { get; } = new NotificationType("biz_account_disabled");

        /// <summary>Gets the biz invitation accepted.</summary>
        public static NotificationType BizInvitationAccepted { get; } = new NotificationType("biz_invitation_accepted");

        /// <summary>Gets the biz invitation created.</summary>
        public static NotificationType BizInvitationCreated { get; } = new NotificationType("biz_invitation_created");

        /// <summary>Gets the biz invitation rejected.</summary>
        public static NotificationType BizInvitationRejected { get; } = new NotificationType("biz_invitation_rejected");

        /// <summary>Gets the biz payment failed.</summary>
        public static NotificationType BizPaymentFailed { get; } = new NotificationType("biz_payment_failed");

        /// <summary>Gets the biz policy disallowed invitation.</summary>
        public static NotificationType BizPolicyDisallowedInvitation { get; } = new NotificationType("biz_policy_disallowed_invitation");

        /// <summary>Gets the biz policy rejected invitation.</summary>
        public static NotificationType BizPolicyRejectedInvitation { get; } = new NotificationType("biz_policy_rejected_invitation");

        /// <summary>Gets the biz trial enter cc.</summary>
        public static NotificationType BizTrialEnterCc { get; } = new NotificationType("biz_trial_enter_cc");

        /// <summary>Gets the biz trial will end.</summary>
        public static NotificationType BizTrialWillEnd { get; } = new NotificationType("biz_trial_will_end");

        /// <summary>Gets the item assigned.</summary>
        public static NotificationType ItemAssigned { get; } = new NotificationType("item_assigned");

        /// <summary>Gets the item completed.</summary>
        public static NotificationType ItemCompleted { get; } = new NotificationType("item_completed");

        /// <summary>Gets the item uncompleted.</summary>
        public static NotificationType ItemUncompleted { get; } = new NotificationType("item_uncompleted");

        /// <summary>Gets the karma level.</summary>
        public static NotificationType KarmaLevel { get; } = new NotificationType("karma_level");

        /// <summary>Gets the message.</summary>
        public static NotificationType Message { get; } = new NotificationType("message");

        /// <summary>Gets the note added.</summary>
        public static NotificationType NoteAdded { get; } = new NotificationType("note_added");

        /// <summary>Gets the pro trial started.</summary>
        public static NotificationType ProTrialStarted { get; } = new NotificationType("pro_trial_started");

        /// <summary>Gets the pro trial ended.</summary>
        public static NotificationType ProTrialEnded { get; } = new NotificationType("pro_trial_ended");

        /// <summary>Gets the project archived.</summary>
        public static NotificationType ProjectArchived { get; } = new NotificationType("project_archived");

        /// <summary>Gets the project moved.</summary>
        public static NotificationType ProjectMoved { get; } = new NotificationType("project_moved");

        /// <summary>Gets the price increase android.</summary>
        public static NotificationType PriceIncreaseAndroid { get; } = new NotificationType("price_increase_android");

        /// <summary>Gets the price increase new pro users.</summary>
        public static NotificationType PriceIncreaseNewProUsers { get; } = new NotificationType("price_increase_new_pro_users");

        /// <summary>Gets the price increase new team.</summary>
        public static NotificationType PriceIncreaseNewTeam { get; } = new NotificationType("price_increase_new_team");

        /// <summary>Gets the price increase new team trial.</summary>
        public static NotificationType PriceIncreaseNewTeamTrial { get; } = new NotificationType("price_increase_new_team_trial");

        /// <summary>Gets the removed from workspace.</summary>
        public static NotificationType RemovedFromWorkspace { get; } = new NotificationType("removed_from_workspace");

        /// <summary>Gets the share invitation accepted.</summary>
        public static NotificationType ShareInvitationAccepted { get; } = new NotificationType("share_invitation_accepted");

        /// <summary>Gets the share invitation blocked by project limit.</summary>
        public static NotificationType ShareInvitationBlockedByProjectLimit { get; } = new NotificationType("share_invitation_blocked_by_project_limit");

        /// <summary>Gets the share invitation rejected.</summary>
        public static NotificationType ShareInvitationRejected { get; } = new NotificationType("share_invitation_rejected");

        /// <summary>Gets the share invitation sent.</summary>
        public static NotificationType ShareInvitationSent { get; } = new NotificationType("share_invitation_sent");

        /// <summary>Gets the teams workspace canceled.</summary>
        public static NotificationType TeamsWorkspaceCanceled { get; } = new NotificationType("teams_workspace_canceled");

        /// <summary>Gets the teams workspace payment failed.</summary>
        public static NotificationType TeamsWorkspacePaymentFailed { get; } = new NotificationType("teams_workspace_payment_failed");

        /// <summary>Gets the teams workspace upgraded.</summary>
        public static NotificationType TeamsWorkspaceUpgraded { get; } = new NotificationType("teams_workspace_upgraded");

        /// <summary>Gets the user left project.</summary>
        public static NotificationType UserLeftProject { get; } = new NotificationType("user_left_project");

        /// <summary>Gets the user removed from project.</summary>
        public static NotificationType UserRemovedFromProject { get; } = new NotificationType("user_removed_from_project");

        /// <summary>Gets the workspace deleted.</summary>
        public static NotificationType WorkspaceDeleted { get; } = new NotificationType("workspace_deleted");

        /// <summary>Gets the workspace invitation accepted.</summary>
        public static NotificationType WorkspaceInvitationAccepted { get; } = new NotificationType("workspace_invitation_accepted");

        /// <summary>Gets the workspace invitation created.</summary>
        public static NotificationType WorkspaceInvitationCreated { get; } = new NotificationType("workspace_invitation_created");

        /// <summary>Gets the workspace invitation rejected.</summary>
        public static NotificationType WorkspaceInvitationRejected { get; } = new NotificationType("workspace_invitation_rejected");

        /// <summary>Gets the workspace team cohort tagged.</summary>
        public static NotificationType WorkspaceTeamCohortTagged { get; } = new NotificationType("workspace_team_cohort_tagged");

        /// <summary>Gets the workspace user joined by domain.</summary>
        public static NotificationType WorkspaceUserJoinedByDomain { get; } = new NotificationType("workspace_user_joined_by_domain");
    }
}
