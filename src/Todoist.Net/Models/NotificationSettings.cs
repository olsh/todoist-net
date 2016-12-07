using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents notification settings.
    /// </summary>
    public class NotificationSettings
    {
        /// <summary>
        /// Gets the biz account disabled.
        /// </summary>
        /// <value>The biz account disabled.</value>
        [JsonProperty("biz_account_disabled")]
        public NotificationSetting BizAccountDisabled { get; internal set; }

        /// <summary>
        /// Gets the biz invitation accepted.
        /// </summary>
        /// <value>The biz invitation accepted.</value>
        [JsonProperty("biz_invitation_accepted")]
        public NotificationSetting BizInvitationAccepted { get; internal set; }

        /// <summary>
        /// Gets the biz invitation rejected.
        /// </summary>
        /// <value>The biz invitation rejected.</value>
        [JsonProperty("biz_invitation_rejected")]
        public NotificationSetting BizInvitationRejected { get; internal set; }

        /// <summary>
        /// Gets the biz payment failed.
        /// </summary>
        /// <value>The biz payment failed.</value>
        [JsonProperty("biz_payment_failed")]
        public NotificationSetting BizPaymentFailed { get; internal set; }

        /// <summary>
        /// Gets the biz trial enter cc.
        /// </summary>
        /// <value>The biz trial enter cc.</value>
        [JsonProperty("biz_trial_enter_cc")]
        public NotificationSetting BizTrialEnterCc { get; internal set; }

        /// <summary>
        /// Gets the biz trial will end.
        /// </summary>
        /// <value>The biz trial will end.</value>
        [JsonProperty("biz_trial_will_end")]
        public NotificationSetting BizTrialWillEnd { get; internal set; }

        /// <summary>
        /// Gets the item assigned.
        /// </summary>
        /// <value>The item assigned.</value>
        [JsonProperty("item_assigned")]
        public NotificationSetting ItemAssigned { get; internal set; }

        /// <summary>
        /// Gets the item completed.
        /// </summary>
        /// <value>The item completed.</value>
        [JsonProperty("item_completed")]
        public NotificationSetting ItemCompleted { get; internal set; }

        /// <summary>
        /// Gets the item uncompleted.
        /// </summary>
        /// <value>The item uncompleted.</value>
        [JsonProperty("item_uncompleted")]
        public NotificationSetting ItemUncompleted { get; internal set; }

        /// <summary>
        /// Gets the note added.
        /// </summary>
        /// <value>The note added.</value>
        [JsonProperty("note_added")]
        public NotificationSetting NoteAdded { get; internal set; }

        /// <summary>
        /// Gets the share invitation accepted.
        /// </summary>
        /// <value>The share invitation accepted.</value>
        [JsonProperty("share_invitation_accepted")]
        public NotificationSetting ShareInvitationAccepted { get; internal set; }

        /// <summary>
        /// Gets the share invitation rejected.
        /// </summary>
        /// <value>The share invitation rejected.</value>
        [JsonProperty("share_invitation_rejected")]
        public NotificationSetting ShareInvitationRejected { get; internal set; }

        /// <summary>
        /// Gets the user left project.
        /// </summary>
        /// <value>The user left project.</value>
        [JsonProperty("user_left_project")]
        public NotificationSetting UserLeftProject { get; internal set; }

        /// <summary>
        /// Gets the user removed from project.
        /// </summary>
        /// <value>The user removed from project.</value>
        [JsonProperty("user_removed_from_project")]
        public NotificationSetting UserRemovedFromProject { get; internal set; }
    }
}
