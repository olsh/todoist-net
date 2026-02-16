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

        /// <summary>
        /// Gets the biz account disabled.
        /// </summary>
        /// <value>The biz account disabled.</value>
        public static NotificationType BizAccountDisabled { get; } = new NotificationType("biz_account_disabled");

        /// <summary>
        /// Gets the biz invitation accepted.
        /// </summary>
        /// <value>The biz invitation accepted.</value>
        public static NotificationType BizInvitationAccepted { get; } = new NotificationType("biz_invitation_accepted");

        /// <summary>
        /// Gets the biz invitation created.
        /// </summary>
        /// <value>The biz invitation created.</value>
        public static NotificationType BizInvitationCreated { get; } = new NotificationType("biz_invitation_created");

        /// <summary>
        /// Gets the biz invitation rejected.
        /// </summary>
        /// <value>The biz invitation rejected.</value>
        public static NotificationType BizInvitationRejected { get; } = new NotificationType("biz_invitation_rejected");

        /// <summary>
        /// Gets the biz payment failed.
        /// </summary>
        /// <value>The biz payment failed.</value>
        public static NotificationType BizPaymentFailed { get; } = new NotificationType("biz_payment_failed");

        /// <summary>
        /// Gets the biz policy disallowed invitation.
        /// </summary>
        /// <value>The biz policy disallowed invitation.</value>
        public static NotificationType BizPolicyDisallowedInvitation { get; } =
            new NotificationType("biz_policy_disallowed_invitation");

        /// <summary>
        /// Gets the biz policy rejected invitation.
        /// </summary>
        /// <value>The biz policy rejected invitation.</value>
        public static NotificationType BizPolicyRejectedInvitation { get; } =
            new NotificationType("biz_policy_rejected_invitation");

        /// <summary>
        /// Gets the biz trial will end.
        /// </summary>
        /// <value>The biz trial will end.</value>
        public static NotificationType BizTrialWillEnd { get; } = new NotificationType("biz_trial_will_end");

        /// <summary>
        /// Gets the item assigned.
        /// </summary>
        /// <value>The item assigned.</value>
        public static NotificationType ItemAssigned { get; } = new NotificationType("item_assigned");

        /// <summary>
        /// Gets the item completed.
        /// </summary>
        /// <value>The item completed.</value>
        public static NotificationType ItemCompleted { get; } = new NotificationType("item_completed");

        /// <summary>
        /// Gets the item uncompleted.
        /// </summary>
        /// <value>The item uncompleted.</value>
        public static NotificationType ItemUncompleted { get; } = new NotificationType("item_uncompleted");

        /// <summary>
        /// Gets the note added.
        /// </summary>
        /// <value>The note added.</value>
        public static NotificationType NoteAdded { get; } = new NotificationType("note_added");

        /// <summary>
        /// Gets the share invitation accepted.
        /// </summary>
        /// <value>The share invitation accepted.</value>
        public static NotificationType ShareInvitationAccepted { get; } =
            new NotificationType("share_invitation_accepted");

        /// <summary>
        /// Gets the share invitation rejected.
        /// </summary>
        /// <value>The share invitation rejected.</value>
        public static NotificationType ShareInvitationRejected { get; } =
            new NotificationType("share_invitation_rejected");

        /// <summary>
        /// Gets the share invitation sent.
        /// </summary>
        /// <value>The share invitation sent.</value>
        public static NotificationType ShareInvitationSent { get; } = new NotificationType("share_invitation_sent");

        /// <summary>
        /// Gets the user left project.
        /// </summary>
        /// <value>The user left project.</value>
        public static NotificationType UserLeftProject { get; } = new NotificationType("user_left_project");

        /// <summary>
        /// Gets the user removed from project.
        /// </summary>
        /// <value>The user removed from project.</value>
        public static NotificationType UserRemovedFromProject { get; } =
            new NotificationType("user_removed_from_project");
    }
}
