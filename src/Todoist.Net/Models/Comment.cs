using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist comment.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.BaseEntity" />
    public class Comment : BaseUnsetEntity, IWithRelationsArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Comment" /> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public Comment(string content)
        {
            Content = content;

            UserIdsToNotify = new Collection<string>();
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the file attachment.
        /// </summary>
        /// <value>The file attachment.</value>
        [JsonPropertyName("file_attachment")]
        public FileAttachment FileAttachment { get; set; }

        /// <summary>
        /// Gets the is deleted.
        /// </summary>
        /// <value>The is deleted.</value>
        [JsonPropertyName("is_deleted")]
        public bool? IsDeleted { get; internal set; }

        /// <summary>
        /// Gets or sets the task identifier.
        /// </summary>
        /// <value>The task identifier.</value>
        /// <remarks>
        /// The JSON property name remains "item_id" for backwards compatibility with Sync API.
        /// </remarks>
        [JsonPropertyName("item_id")]
        public ComplexId? TaskId { get; set; }

        /// <summary>
        /// Gets the posted.
        /// </summary>
        /// <value>The posted.</value>
        [JsonPropertyName("posted_at")]
        public DateTime? PostedAt { get; internal set; }

        /// <summary>
        /// Gets the posted user identifier.
        /// </summary>
        /// <value>The posted user identifier.</value>
        [JsonPropertyName("posted_uid")]
        public string PostedUserId { get; internal set; }

        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        /// <value>The project identifier.</value>
        [JsonPropertyName("project_id")]
        public ComplexId? ProjectId { get; internal set; }

        /// <summary>
        /// Gets the user ids to notify.
        /// </summary>
        /// <value>The user ids to notify.</value>
        [JsonPropertyName("uids_to_notify")]
        public ICollection<string> UserIdsToNotify { get; internal set; }

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

            if (TaskId.HasValue && map.TryGetValue(TaskId.Value.TempId, out var persistentTaskId))
            {
                TaskId = new ComplexId(persistentTaskId);
            }
        }
    }
}
