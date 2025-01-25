using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a base for Todoist tasks.
    /// </summary>
    public abstract class BaseItem : BaseUnsetEntity
    {
        private protected BaseItem(ComplexId id)
            : base(id)
        {
        }

        private protected BaseItem()
        {
        }

        /// <summary>
        /// Gets or sets the assigned by uid.
        /// </summary>
        /// <value>The assigned by uid.</value>
        [JsonPropertyName("assigned_by_uid")]
        public string AssignedByUid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Item" /> is collapsed.
        /// </summary>
        /// <value><c>null</c> if [collapsed] contains no value, <c>true</c> if [collapsed]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("collapsed")]
        public bool? Collapsed { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the day order.
        /// </summary>
        /// <value>The day order.</value>
        [JsonPropertyName("day_order")]
        public int? DayOrder { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        [JsonPropertyName("due")]
        public DueDate DueDate { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <remarks>
        /// Durations are only available for Todoist Premium users.
        /// </remarks>
        /// <value>
        /// The duration.
        /// </value>
        [JsonPropertyName("duration")]
        public Duration Duration { get; set; }

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <value>The labels.</value>
        [JsonPropertyName("labels")]
        public ICollection<string> Labels { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        [JsonPropertyName("priority")]
        public Priority? Priority { get; set; }

        /// <summary>
        /// Gets or sets the responsible uid.
        /// </summary>
        /// <value>The responsible uid.</value>
        [JsonPropertyName("responsible_uid")]
        public string ResponsibleUid { get; set; }

        /// <summary>
        /// Gets or sets the deadline for the task.
        /// </summary>
        /// <value>The deadline.</value>
        [JsonPropertyName("deadline")]
        public Deadline Deadline { get; set; }
    }
}
