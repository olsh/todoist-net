using System.Collections.Generic;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents base properties of a Todoist task.
    /// </summary>
    public class ItemBase : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemBase" /> class.
        /// </summary>
        /// <param name="itemId">The item (task) identifier.</param>
        public ItemBase(ComplexId itemId)
        {
            Id = itemId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemBase" /> class.
        /// </summary>
        /// <param name="itemId">The item (task) identifier.</param>
        /// <param name="content">The content.</param>
        public ItemBase(ComplexId itemId, string content)
            : this(itemId)
        {
            Content = content;
        }

        /// <summary>
        /// Gets or sets the assigned by uid.
        /// </summary>
        /// <value>The assigned by uid.</value>
        [JsonProperty("assigned_by_uid")]
        public string AssignedByUid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this task is collapsed.
        /// </summary>
        /// <value><c>null</c> if [collapsed] contains no value, <c>true</c> if [collapsed]; otherwise, <c>false</c>.</value>
        [JsonProperty("collapsed")]
        public bool? Collapsed { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the day order.
        /// </summary>
        /// <value>The day order.</value>
        [JsonProperty("day_order")]
        public int? DayOrder { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        [JsonProperty("due")]
        public DueDate DueDate { get; set; } = DueDate.Default;
        
        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <remarks>
        /// Durations are only available for Todoist Premium users.
        /// </remarks>
        /// <value>
        /// The duration.
        /// </value>
        [JsonProperty("duration")]
        public Duration Duration { get; set; } = Duration.Default;

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <value>The labels.</value>
        [JsonProperty("labels")]
        public ICollection<string> Labels { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        [JsonProperty("priority")]
        public Priority? Priority { get; set; }

        /// <summary>
        /// Gets or sets the responsible uid.
        /// </summary>
        /// <value>The responsible uid.</value>
        [JsonProperty("responsible_uid")]
        public string ResponsibleUid { get; set; }

    }
}
