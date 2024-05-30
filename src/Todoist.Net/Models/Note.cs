using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist note.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.BaseEntity" />
    public class Note : BaseUnsetEntity, IWithRelationsArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Note" /> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public Note(string content)
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
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>The item identifier.</value>
        [JsonPropertyName("item_id")]
        public ComplexId? ItemId { get; set; }

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

            if (ItemId.HasValue && map.TryGetValue(ItemId.Value.TempId, out var persistentItemId))
            {
                ItemId = new ComplexId(persistentItemId);
            }
        }
    }
}
