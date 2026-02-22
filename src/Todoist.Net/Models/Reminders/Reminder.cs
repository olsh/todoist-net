using System.Text.Json.Serialization;

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
        /// <param name="id">The ID of a reminder.</param>
        public Reminder(ComplexId id)
            : base(id)
        {
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
        /// Gets or sets the task identifier.
        /// </summary>
        /// <value>The task identifier.</value>
        /// <remarks>The JSON property name is "item_id" in order to be compatible with the API.</remarks>
        [JsonPropertyName("item_id")]
        public ComplexId TaskId { get; set; }

        /// <summary>
        /// Gets or sets the minute offset.
        /// </summary>
        /// <value>The minute offset.</value>
        /// <remarks>
        /// The relative time in minutes before the due date of the item, in which the reminder should be triggered.
        /// Note, that the item should have a due date set in order to add a relative reminder.
        /// </remarks>
        [JsonPropertyName("minute_offset")]
        public long? MinuteOffset { get; set; }

        /// <summary>
        /// Gets or sets the notify uid.
        /// </summary>
        /// <value>The notify uid.</value>
        [JsonPropertyName("notify_uid")]
        public string NotifyUid { get; set; }

        /// <summary>
        /// Gets or sets the alias name of the reminder.
        /// </summary>
        /// <value>The alias name of the reminder.</value>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the reminder's location.
        /// </summary>
        /// <value>The latitude of the reminder's location.</value>
        [JsonPropertyName("loc_lat")]
        public string LocationLatitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the reminder's location.
        /// </summary>
        /// <value>The longitude of the reminder's location.</value>
        [JsonPropertyName("loc_long")]
        public string LocationLongitude { get; set; }

        /// <summary>
        /// Gets or sets the radius around the location that is still considered as part of the location (in meters).
        /// </summary>
        /// <value>The radius of the reminder's location.</value>
        [JsonPropertyName("radius")]
        public long? Radius { get; set; }

        /// <summary>
        /// Gets or sets the location trigger.
        /// </summary>
        /// <value>The location trigger.</value>
        [JsonPropertyName("loc_trigger")]
        public LocationTrigger LocationTrigger { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [JsonPropertyName("type")]
        public ReminderType Type { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        [JsonPropertyName("is_deleted")]
        public bool? IsDeleted { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this reminder is urgent.
        /// </summary>
        /// <value><c>true</c> if this reminder is urgent; otherwise, <c>false</c>.</value>
        [JsonPropertyName("is_urgent")]
        public bool? IsUrgent { get; internal set; }
    }
}
