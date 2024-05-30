using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist task.
    /// </summary>
    public class Item : UpdateItem
    {
        [JsonConstructor]
        internal Item()
            : base(default)
        {
        }

        /// <summary>
        /// Gets or sets the added by uid.
        /// </summary>
        /// <value>The added by uid.</value>
        [JsonPropertyName("added_by_uid")]
        public string AddedByUid { get; set; }

        /// <summary>
        /// Gets or sets order of project. Defines the position of the project among all the projects with the same parent_id.
        /// </summary>
        /// <value>The project order.</value>
        [JsonPropertyName("child_order")]
        public int? ChildOrder { get; set; }

        /// <summary>
        /// Gets the date added.
        /// </summary>
        /// <value>The date added.</value>
        [JsonPropertyName("added_at")]
        public DateTime? AddedAt { get; internal set; }

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

        /// <summary>
        /// Gets or sets the id of the parent task. Set to <see langword="null" /> for root tasks.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        [JsonPropertyName("parent_id")]
        public string ParentId { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>The project identifier.</value>
        [JsonPropertyName("project_id")]
        public ComplexId? ProjectId { get; set; }

        /// <summary>
        /// Gets or sets section of project. Defines the section that the task belongs to.
        /// </summary>
        /// <value>The project order.</value>
        [JsonPropertyName("section_id")]
        public string Section { get; set; }


        /// <summary>
        /// Gets the synchronize identifier.
        /// </summary>
        /// <value>The synchronize identifier.</value>
        [JsonPropertyName("sync_id")]
        public string SyncId { get; internal set; }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [JsonPropertyName("user_id")]
        public string UserId { get; internal set; }

    }
}
