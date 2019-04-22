using Newtonsoft.Json;

using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Class Reminder.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.BaseEntity" />
    public class Reminder : BaseEntity
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
        internal Reminder()
        {
        }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        [JsonProperty("due")]
        public DueDate DueDate { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("is_deleted")]
        public bool? IsDeleted { get; internal set; }

        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>The item identifier.</value>
        [JsonProperty("item_id")]
        public ComplexId ItemId { get; set; }

        /// <summary>
        /// Gets or sets the location trigger.
        /// </summary>
        /// <value>The location trigger.</value>
        [JsonProperty("loc_trigger")]
        public LocationTrigger LocationTrigger { get; set; }

        /// <summary>
        /// Gets or sets the mm offset.
        /// </summary>
        /// <value>The mm offset.</value>
        /// <remarks>The relative time in minutes before the due date of the item, in which the reminder should be triggered.
        /// Note, that the item should have a due date set in order to add a relative reminder.</remarks>
        [JsonProperty("mm_offset")]
        public long? MinuteOffset { get; set; }

        /// <summary>
        /// Gets or sets the notify uid.
        /// </summary>
        /// <value>The notify uid.</value>
        [JsonProperty("notify_uid")]
        public long? NotifyUid { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>The service.</value>
        [JsonProperty("service")]
        public ReminderService Service { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [JsonProperty("type")]
        public ReminderType Type { get; set; }
    }
}
