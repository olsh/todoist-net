using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a base for Todoist tasks.
    /// </summary>
    public abstract class BaseTask : BaseUnsetEntity
    {
        private protected BaseTask(ComplexId id)
            : base(id)
        {
        }

        private protected BaseTask()
        {
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonPropertyName("description")]
        public string Description { get; set; }

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
        /// Gets or sets the deadline for the task.
        /// </summary>
        /// <value>The deadline.</value>
        [JsonPropertyName("deadline")]
        public Deadline Deadline { get; set; }

        /// <summary>
        /// Gets or sets the responsible uid.
        /// </summary>
        /// <value>The responsible uid.</value>
        [JsonPropertyName("responsible_uid")]
        public string ResponsibleUid { get; set; }

        /// <summary>
        /// Gets or sets the assigned by uid.
        /// </summary>
        /// <value>The assigned by uid.</value>
        [JsonPropertyName("assigned_by_uid")]
        public string AssignedByUid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this task is collapsed.
        /// </summary>
        /// <value><c>null</c> if [is_collapsed] contains no value, <c>true</c> if [is_collapsed]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("is_collapsed")]
        public bool? IsCollapsed { get; set; }

        /// <summary>
        /// Gets or sets the day order.
        /// </summary>
        /// <value>The day order.</value>
        [JsonPropertyName("day_order")]
        public int? DayOrder { get; set; }

        /// <summary>
        /// Gets or sets the fractional-indexing key which orders the task among its siblings.
        /// </summary>
        /// <remarks>
        /// Keys sort as ordinal strings. A task added without a key goes to the bottom, and an update without one keeps the current key.
        /// If a sibling already uses the key, Todoist stores a key right after it instead, which a sync returns.
        /// Todoist returns <c>null</c> for tasks it hasn't migrated to keys yet.
        /// </remarks>
        /// <value>The order key.</value>
        [JsonPropertyName("order_key")]
        public string OrderKey { get; set; }
    }
}
