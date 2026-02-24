using System.Text.Json.Serialization;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a workspace folder.
    /// </summary>
    public class WorkspaceFolder : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceFolder"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="childOrder">The child order.</param>
        /// <param name="defaultOrder">The default order.</param>
        public WorkspaceFolder(string name, int? childOrder = null, int? defaultOrder = null)
        {
            ThrowHelper.ThrowIfNullOrEmpty(name, nameof(name));

            Name = name;
            ChildOrder = childOrder ?? 0;
            DefaultOrder = defaultOrder ?? -1;
        }

        [JsonConstructor]
        internal WorkspaceFolder()
        {
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets the child order.
        /// </summary>
        /// <value>The child order.</value>
        [JsonPropertyName("child_order")]
        public int ChildOrder { get; set; }

        /// <summary>
        /// Gets the default order.
        /// </summary>
        /// <value>The default order.</value>
        [JsonPropertyName("default_order")]
        public int DefaultOrder { get; set; }

        /// <summary>
        /// Gets the workspace ID.
        /// </summary>
        /// <value>The workspace ID.</value>
        [JsonPropertyName("workspace_id")]
        public ComplexId WorkspaceId { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the folder is deleted.
        /// </summary>
        /// <value>Indicates whether the folder is deleted.</value>
        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; internal set; }
    }
}
