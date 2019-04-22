using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist note.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.BaseEntity" />
    public class Note : BaseEntity, IWithRelationsArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Note" /> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public Note(string content)
        {
            Content = content;

            UserIdsToNotify = new Collection<int>();
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the file attachment.
        /// </summary>
        /// <value>The file attachment.</value>
        [JsonProperty("file_attachment")]
        public FileAttachment FileAttachment { get; set; }

        /// <summary>
        /// Gets the is archived.
        /// </summary>
        /// <value>The is archived.</value>
        [JsonProperty("is_archived")]
        [JsonConverter(typeof(BoolConverter))]
        public bool? IsArchived { get; internal set; }

        /// <summary>
        /// Gets the is deleted.
        /// </summary>
        /// <value>The is deleted.</value>
        [JsonProperty("is_deleted")]
        [JsonConverter(typeof(BoolConverter))]
        public bool? IsDeleted { get; internal set; }

        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>The item identifier.</value>
        [JsonProperty("item_id")]
        public ComplexId? ItemId { get; set; }

        /// <summary>
        /// Gets the posted.
        /// </summary>
        /// <value>The posted.</value>
        [JsonProperty("posted")]
        public DateTime? Posted { get; internal set; }

        /// <summary>
        /// Gets the posted user identifier.
        /// </summary>
        /// <value>The posted user identifier.</value>
        [JsonProperty("posted_uid")]
        public long? PostedUserId { get; internal set; }

        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        /// <value>The project identifier.</value>
        [JsonProperty("project_id")]
        public ComplexId? ProjectId { get; internal set; }

        /// <summary>
        /// Gets the user ids to notify.
        /// </summary>
        /// <value>The user ids to notify.</value>
        [JsonProperty("uids_to_notify")]
        public ICollection<int> UserIdsToNotify { get; internal set; }

        /// <summary>
        /// Updates the related temporary ids.
        /// </summary>
        /// <param name="map">The map.</param>
        void IWithRelationsArgument.UpdateRelatedTempIds(IDictionary<Guid, long> map)
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
