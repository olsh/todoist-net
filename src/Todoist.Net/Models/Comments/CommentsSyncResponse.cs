using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a response that contains synchronized comments data.
    /// </summary>
    public class CommentsSyncResponse : BaseSyncResponse
    {
        /// <summary>Gets the comments.</summary>
        /// <remarks>The JSON property name remains "notes" for backwards compatibility with Sync API.</remarks>
        [JsonPropertyName("notes")]
        public IReadOnlyCollection<Comment> Comments { get; internal set; }

        /// <summary>Gets the project comments.</summary>
        /// <remarks>The JSON property name remains "project_notes" for backwards compatibility with Sync API.</remarks>
        [JsonPropertyName("project_notes")]
        public IReadOnlyCollection<Comment> ProjectComments { get; internal set; }

        /// <summary>Gets the incomplete task IDs.</summary>
        /// <remarks>The JSON property name remains "incomplete_item_ids" for backwards compatibility with Sync API.</remarks>
        [JsonPropertyName("incomplete_item_ids")]
        public IReadOnlyCollection<string> IncompleteTaskIds { get; internal set; }

        /// <summary>Gets the incomplete project IDs.</summary>
        [JsonPropertyName("incomplete_project_ids")]
        public IReadOnlyCollection<string> IncompleteProjectIds { get; internal set; }
    }
}
