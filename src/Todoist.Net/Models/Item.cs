using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist task.
    /// </summary>
    public class Item : BaseEntity, IWithRelationsArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public Item(string content)
            // ReSharper disable once IntroduceOptionalParameters.Global
            : this(content, default(ComplexId))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item" /> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="projectId">The project identifier.</param>
        public Item(string content, ComplexId projectId)
        {
            Content = content;
            ProjectId = projectId;
            Labels = new Collection<int>();
        }

        internal Item()
        {
        }

        /// <summary>
        /// Gets or sets the assigned by uid.
        /// </summary>
        /// <value>The assigned by uid.</value>
        [JsonProperty("assigned_by_uid")]
        public long? AssignedByUid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Item"/> is collapsed.
        /// </summary>
        /// <value><c>null</c> if [collapsed] contains no value, <c>true</c> if [collapsed]; otherwise, <c>false</c>.</value>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("collapsed")]
        public bool? Collapsed { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets the date added.
        /// </summary>
        /// <value>The date added.</value>
        [JsonProperty("date_added")]
        public DateTime? DateAdded { get; internal set; }

        /// <summary>
        /// Gets or sets the date language.
        /// </summary>
        /// <value>The date language.</value>
        [JsonProperty("date_lang")]
        public Language DateLanguage { get; set; }

        /// <summary>
        /// Gets or sets the date string.
        /// </summary>
        /// <value>The date string.</value>
        [JsonProperty("date_string")]
        public string DateString { get; set; }

        /// <summary>
        /// Gets or sets the day order.
        /// </summary>
        /// <value>The day order.</value>
        [JsonProperty("day_order")]
        public int? DayOrder { get; set; }

        /// <summary>
        /// Gets or sets the due date UTC.
        /// </summary>
        /// <value>The due date UTC.</value>
        [JsonProperty("due_date_utc")]
        [JsonConverter(typeof(UtcDateTimeConverter))]
        public DateTime? DueDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the indent.
        /// </summary>
        /// <value>The indent.</value>
        [JsonProperty("indent")]
        public int? Indent { get; set; }

        /// <summary>
        /// Gets a value indicating whether [in history].
        /// </summary>
        /// <value><c>null</c> if [in history] contains no value, <c>true</c> if [in history]; otherwise, <c>false</c>.</value>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("in_history")]
        public bool? InHistory { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is archived.
        /// </summary>
        /// <value><c>null</c> if [is archived] contains no value, <c>true</c> if [is archived]; otherwise, <c>false</c>.</value>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("is_archived")]
        public bool? IsArchived { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is checked.
        /// </summary>
        /// <value><c>null</c> if [is checked] contains no value, <c>true</c> if [is checked]; otherwise, <c>false</c>.</value>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("checked")]
        public bool? IsChecked { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>null</c> if [is deleted] contains no value, <c>true</c> if [is deleted]; otherwise, <c>false</c>.</value>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("is_deleted")]
        public bool? IsDeleted { get; internal set; }

        /// <summary>
        /// Gets or sets the item order.
        /// </summary>
        /// <value>The item order.</value>
        [JsonProperty("item_order")]
        public int? ItemOrder { get; set; }

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <value>The labels.</value>
        [JsonProperty("labels")]
        public ICollection<int> Labels { get; internal set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        [JsonProperty("priority")]
        public Priority? Priority { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>The project identifier.</value>
        [JsonProperty("project_id")]
        public ComplexId? ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the responsible uid.
        /// </summary>
        /// <value>The responsible uid.</value>
        [JsonProperty("responsible_uid")]
        public long? ResponsibleUid { get; set; }

        /// <summary>
        /// Gets the synchronize identifier.
        /// </summary>
        /// <value>The synchronize identifier.</value>
        [JsonProperty("sync_id")]
        public long? SyncId { get; internal set; }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [JsonProperty("user_id")]
        public long? UserId { get; internal set; }

        /// <summary>
        /// Updates the related temporary ids.
        /// </summary>
        /// <param name="map">The map.</param>
        void IWithRelationsArgument.UpdateRelatedTempIds(IDictionary<Guid, long> map)
        {
            long persistentProjectId;
            if (ProjectId.HasValue && map.TryGetValue(ProjectId.Value.TempId, out persistentProjectId))
            {
                ProjectId = new ComplexId(persistentProjectId);
            }
        }
    }
}
