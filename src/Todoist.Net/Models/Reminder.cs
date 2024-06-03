using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Class Reminder.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.BaseEntity" />
    public class Reminder : BaseUnsetEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Reminder" /> class.
        /// </summary>
        /// <param name="itemId">An ID of a task.</param>
        public Reminder(ComplexId itemId)
        {
            ItemId = itemId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reminder"/> class.
        /// </summary>
        [JsonConstructor]
        internal Reminder()
        {
        }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        [JsonPropertyName("due")]
        public DueDate DueDate { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        [JsonPropertyName("is_deleted")]
        public bool? IsDeleted { get; internal set; }

        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>The item identifier.</value>
        [JsonPropertyName("item_id")]
        public ComplexId ItemId { get; set; }

        /// <summary>
        /// Gets or sets the location trigger.
        /// </summary>
        /// <value>The location trigger.</value>
        [JsonPropertyName("loc_trigger")]
        public LocationTrigger LocationTrigger { get; set; }

        /// <summary>
        /// Gets or sets the mm offset.
        /// </summary>
        /// <value>The mm offset.</value>
        /// <remarks>The relative time in minutes before the due date of the item, in which the reminder should be triggered.
        /// Note, that the item should have a due date set in order to add a relative reminder.</remarks>
        [JsonPropertyName("minute_offset")]
        public long? MinuteOffset { get; set; }

        /// <summary>
        /// Gets or sets the notify uid.
        /// </summary>
        /// <value>The notify uid.</value>
        [JsonPropertyName("notify_uid")]
        public string NotifyUid { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [JsonPropertyName("type")]
        public ReminderType Type { get; set; }
    }
}
