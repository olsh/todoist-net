using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist task with full details.
    /// </summary>
    public class TaskInfo : UpdateTask
    {
        [JsonConstructor]
        internal TaskInfo()
            : base(default)
        {
        }

        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        /// <value>The project identifier.</value>
        [JsonPropertyName("project_id")]
        public string ProjectId { get; internal set; }

        /// <summary>
        /// Gets the section of the project. Defines the section that the task belongs to.
        /// </summary>
        /// <value>The section identifier.</value>
        [JsonPropertyName("section_id")]
        public string SectionId { get; internal set; }

        /// <summary>
        /// Gets the id of the parent task. Set to <see langword="null" /> for root tasks.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        [JsonPropertyName("parent_id")]
        public string ParentId { get; internal set; }

        /// <summary>
        /// Gets the order of the project. Defines the position of the project among all the projects with the same parent_id.
        /// </summary>
        /// <value>The project order.</value>
        [JsonPropertyName("child_order")]
        public int? ChildOrder { get; internal set; }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [JsonPropertyName("user_id")]
        public string UserId { get; internal set; }
        
        /// <summary>
        /// Gets the user Id of the person who completed the task.
        /// </summary>
        /// <value>The completed by uid.</value>
        [JsonPropertyName("completed_by_uid")]
        public string CompletedByUid { get; internal set; }

        /// <summary>
        /// Gets the date completed.
        /// </summary>
        /// <value>The date completed.</value>
        [JsonPropertyName("completed_at")]
        public DateTime? CompletedAt { get; internal set; }

        /// <summary>
        /// Gets the user Id of the person who added the task.
        /// </summary>
        /// <value>The added by uid.</value>
        [JsonPropertyName("added_by_uid")]
        public string AddedByUid { get; internal set; }

        /// <summary>
        /// Gets the date added.
        /// </summary>
        /// <value>The date added.</value>
        [JsonPropertyName("added_at")]
        public DateTime? AddedAt { get; internal set; }

        /// <summary>
        /// Gets the date updated.
        /// </summary>
        /// <value>The date updated.</value>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is checked.
        /// </summary>
        /// <value><c>null</c> if [is checked] contains no value, <c>true</c> if [is checked]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("checked")]
        public bool? IsChecked { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>null</c> if [is deleted] contains no value, <c>true</c> if [is deleted]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("is_deleted")]
        public bool? IsDeleted { get; internal set; }
    }
}
