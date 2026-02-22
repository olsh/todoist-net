using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a workspace folder.
    /// </summary>
    public class WorkspaceFolder
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        /// <value>The ID.</value>
        [JsonPropertyName("id")]
        public string Id { get; internal set; }

        /// <summary>
        /// Gets the workspace ID.
        /// </summary>
        /// <value>The workspace ID.</value>
        [JsonPropertyName("workspace_id")]
        public string WorkspaceId { get; internal set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonPropertyName("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the folder is deleted.
        /// </summary>
        /// <value>Indicates whether the folder is deleted.</value>
        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; internal set; }

        /// <summary>
        /// Gets the child order.
        /// </summary>
        /// <value>The child order.</value>
        [JsonPropertyName("child_order")]
        public int ChildOrder { get; internal set; }

        /// <summary>
        /// Gets the default order.
        /// </summary>
        /// <value>The default order.</value>
        [JsonPropertyName("default_order")]
        public int DefaultOrder { get; internal set; }
    }
}
