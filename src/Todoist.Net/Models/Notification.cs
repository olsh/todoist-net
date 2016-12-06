using Newtonsoft.Json;

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
        [JsonProperty("created")]
        public long Created { get; internal set; }

        /// <summary>
        /// Gets from uid.
        /// </summary>
        /// <value>From uid.</value>
        [JsonProperty("from_uid")]
        public long FromUid { get; internal set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")]
        public long Id { get; internal set; }

        /// <summary>
        /// Gets the invitation identifier.
        /// </summary>
        /// <value>The invitation identifier.</value>
        [JsonProperty("invitation_id")]
        public long InvitationId { get; internal set; }

        /// <summary>
        /// Gets the notification key.
        /// </summary>
        /// <value>The notification key.</value>
        [JsonProperty("notification_key")]
        public string NotificationKey { get; internal set; }

        /// <summary>
        /// Gets the type of the notification.
        /// </summary>
        /// <value>The type of the notification.</value>
        [JsonProperty("notification_type")]
        public string NotificationType { get; internal set; }

        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        /// <value>The project identifier.</value>
        [JsonProperty("project_id")]
        public long ProjectId { get; internal set; }

        /// <summary>
        /// Gets the reject email.
        /// </summary>
        /// <value>The reject email.</value>
        [JsonProperty("reject_email")]
        public string RejectEmail { get; internal set; }

        /// <summary>
        /// Gets the seq no.
        /// </summary>
        /// <value>The seq no.</value>
        [JsonProperty("seq_no")]
        public long SeqNo { get; internal set; }
    }
}
