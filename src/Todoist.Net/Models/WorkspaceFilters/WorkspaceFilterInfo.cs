using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents workspace filter information returned by get requests.
    /// </summary>
    public class WorkspaceFilterInfo : BaseWorkspaceFilter
    {
        /// <summary>
        /// Gets or sets the ID of the workspace this filter belongs to.
        /// </summary>
        [JsonPropertyName("workspace_id")]
        public ComplexId WorkspaceId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the filter is a favorite for the requesting user.
        /// </summary>
        [JsonPropertyName("is_favorite")]
        public bool? IsFavorite { get; set; }
        
        /// <summary>
        /// Gets a value indicating whether the filter is marked as deleted.
        /// </summary>
        [JsonPropertyName("is_deleted")]
        public bool? IsDeleted { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the filter is frozen and cannot be changed.
        /// </summary>
        [JsonPropertyName("is_frozen")]
        public bool? IsFrozen { get; internal set; }

        /// <summary>
        /// Gets the ID of the user that created the workspace filter.
        /// </summary>
        [JsonPropertyName("creator_uid")]
        public string CreatorUid { get; internal set; }

        /// <summary>
        /// Gets the ID of the user that last updated the workspace filter.
        /// </summary>
        [JsonPropertyName("updater_uid")]
        public string UpdaterUid { get; internal set; }

        /// <summary>
        /// Gets the date when the workspace filter was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; internal set; }

        /// <summary>
        /// Gets the date when the workspace filter was last updated.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; internal set; }
    }
}
