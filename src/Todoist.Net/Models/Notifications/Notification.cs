using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist live notification.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Gets the created.
        /// </summary>
        /// <value>The created.</value>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; internal set; }

        /// <summary>
        /// Gets the ID of the user who initiated this live notification.
        /// </summary>
        /// <value>The ID of the user who initiated this live notification.</value>
        [JsonPropertyName("from_uid")]
        public string FromUid { get; internal set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonPropertyName("id")]
        public string Id { get; internal set; }

        /// <summary>
        /// Gets the notification key.
        /// </summary>
        /// <value>The notification key.</value>
        [JsonPropertyName("notification_key")]
        public string NotificationKey { get; internal set; }

        /// <summary>
        /// Gets the type of the notification.
        /// </summary>
        /// <value>The type of the notification.</value>
        [JsonPropertyName("notification_type")]
        public NotificationType NotificationType { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the notification is deleted.
        /// </summary>
        /// <value><c>true</c> if the notification is deleted; otherwise, <c>false</c>.</value>
        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the notification is unread.
        /// </summary>
        /// <value><c>true</c> if the notification is unread; otherwise, <c>false</c>.</value>
        [JsonPropertyName("is_unread")]
        public bool IsUnread { get; internal set; }

        /// <summary>
        /// Gets the user summary of the user who initiated this live notification.
        /// </summary>
        [JsonPropertyName("from_user")]
        public UserSummary FromUser { get; internal set; }

        /// <summary>
        /// Gets the workspace ID related to this live notification.
        /// </summary>
        /// <remarks>
        /// Available only for <c>workspace_invitation_*</c> types of notifications.
        /// </remarks>
        [JsonPropertyName("workspace_id")]
        public long? WorkspaceId { get; internal set; }

        /// <summary>
        /// Gets the workspace name related to this live notification.
        /// </summary>
        /// <remarks>
        /// Available only for <c>workspace_*</c>, <c>workspace_invitation_*</c>, and <c>teams_workspace_*</c> types of notifications.
        /// </remarks>
        [JsonPropertyName("workspace_name")]
        public string WorkspaceName { get; internal set; }

        /// <summary>
        /// Gets the project name of the invitation related to this live notification.
        /// </summary>
        /// <remarks>
        /// Available only for <c>*_invitation_*</c> types of notifications.
        /// </remarks>
        [JsonPropertyName("project_name")]
        public string ProjectName { get; internal set; }

        /// <summary>
        /// Gets the ID of the invitation related to this live notification.
        /// </summary>
        /// <remarks>
        /// Available only for <c>*_invitation_*</c> types of notifications.
        /// </remarks>
        [JsonPropertyName("invitation_id")]
        public string InvitationId { get; internal set; }

        /// <summary>
        /// Gets the secret of the invitation related to this live notification.
        /// </summary>
        /// <remarks>
        /// Available only for <c>*_invitation_*</c> types of notifications.
        /// </remarks>
        [JsonPropertyName("invitation_secret")]
        public string InvitationSecret { get; internal set; }

        /// <summary>
        /// Gets the state of the invitation related to this live notification.
        /// </summary>
        /// <remarks>
        /// Available only for <c>share_invitation_sent</c> and <c>workspace_invitation_created</c> types of notifications.
        /// </remarks>
        [JsonPropertyName("state")]
        public string State { get; internal set; }

        /// <summary>
        /// Gets the plan type of the invitation related to this live notification.
        /// </summary>
        /// <remarks>
        /// Available only for <c>share_invitation_sent</c> and <c>workspace_invitation_created</c> types of notifications.
        /// </remarks>
        [JsonPropertyName("plan_type")]
        public string PlanType { get; internal set; }

        /// <summary>
        /// Gets the name of the user who was removed from the project related to this live notification.
        /// </summary>
        /// <remarks>
        /// Available only for <c>user_removed_from_project</c> type of notifications.
        /// </remarks>
        [JsonPropertyName("removed_name")]
        public string RemovedName { get; internal set; }

        /// <summary>
        /// Gets the ID of the user who was removed from the project related to this live notification.
        /// </summary>
        /// <remarks>
        /// Available only for <c>user_removed_from_project</c> type of notifications.
        /// </remarks>
        [JsonPropertyName("removed_uid")]
        public string RemovedUid { get; internal set; }
    }
}
