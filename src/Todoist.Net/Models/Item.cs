using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist task.
    /// </summary>
    public class Item : BaseEntity, IWithRelationsArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item" /> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public Item(string content)

            // ReSharper disable once IntroduceOptionalParameters.Global
            : this(content, default)
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
            Labels = new Collection<string>();
        }

        internal Item()
        {
        }

        /// <summary>
        /// Gets or sets the added by uid.
        /// </summary>
        /// <value>The added by uid.</value>
        [JsonProperty("added_by_uid")]
        public string AddedByUid { get; set; }

        /// <summary>
        /// Gets or sets the assigned by uid.
        /// </summary>
        /// <value>The assigned by uid.</value>
        [JsonProperty("assigned_by_uid")]
        public string AssignedByUid { get; set; }

        /// <summary>
        /// Gets or sets order of project. Defines the position of the project among all the projects with the same parent_id.
        /// </summary>
        /// <value>The project order.</value>
        [JsonProperty("child_order")]
        public int? ChildOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Item" /> is collapsed.
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
        /// Gets the date added.
        /// </summary>
        /// <value>The date added.</value>
        [JsonProperty("added_at")]
        public DateTime? AddedAt { get; internal set; }

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
        public DueDate DueDate { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is checked.
        /// </summary>
        /// <value><c>null</c> if [is checked] contains no value, <c>true</c> if [is checked]; otherwise, <c>false</c>.</value>
        [JsonProperty("checked")]
        public bool? IsChecked { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>null</c> if [is deleted] contains no value, <c>true</c> if [is deleted]; otherwise, <c>false</c>.</value>
        [JsonProperty("is_deleted")]
        public bool? IsDeleted { get; internal set; }

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <value>The labels.</value>
        [JsonProperty("labels")]
        public ICollection<string> Labels { get; internal set; }

        /// <summary>
        /// Gets or sets the id of the parent task. Set to <see langword="null" /> for root tasks.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

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
        public string ResponsibleUid { get; set; }

        /// <summary>
        /// Gets or sets section of project. Defines the section that the task belongs to.
        /// </summary>
        /// <value>The project order.</value>
        [JsonProperty("section_id")]
        public string Section { get; set; }


        /// <summary>
        /// Gets the synchronize identifier.
        /// </summary>
        /// <value>The synchronize identifier.</value>
        [JsonProperty("sync_id")]
        public string SyncId { get; internal set; }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [JsonProperty("user_id")]
        public string UserId { get; internal set; }

        /// <summary>
        /// Updates the related temporary ids.
        /// </summary>
        /// <param name="map">The map.</param>
        void IWithRelationsArgument.UpdateRelatedTempIds(IDictionary<Guid, string> map)
        {
            if (ProjectId.HasValue && map.TryGetValue(ProjectId.Value.TempId, out var persistentProjectId))
            {
                ProjectId = new ComplexId(persistentProjectId);
            }
        }
    }
}
