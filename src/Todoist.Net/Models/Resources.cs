using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represent a collection of Todoist resources.
    /// </summary>
    public class Resources
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        [JsonProperty("items")]
        public Item[] Items { get; internal set; }

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <value>The labels.</value>
        [JsonProperty("labels")]
        public Label[] Labels { get; internal set; }

        /// <summary>
        /// Gets or sets the last read notification identifier.
        /// </summary>
        /// <value>The last read notification identifier.</value>
        [JsonProperty("live_notifications_last_read_id")]
        public int? LastReadNotificationId { get; set; }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <value>The notes.</value>
        [JsonProperty("notes")]
        public Note[] Notes { get; internal set; }

        /// <summary>
        /// Gets the notifications.
        /// </summary>
        /// <value>The notifications.</value>
        [JsonProperty("live_notifications")]
        public Notification[] Notifications { get; internal set; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>The projects.</value>
        [JsonProperty("projects")]
        public Project[] Projects { get; internal set; }
    }
}
