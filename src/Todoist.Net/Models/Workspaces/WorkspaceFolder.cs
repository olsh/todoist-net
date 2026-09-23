using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a workspace folder.
    /// </summary>
    public class WorkspaceFolder : BaseEntity, IWithRelationsArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceFolder"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultOrder">The default order.</param>
        public WorkspaceFolder(string name, int? defaultOrder = null)
        {
            ThrowHelper.ThrowIfNullOrEmpty(name, nameof(name));

            Name = name;
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
        /// Gets the default order.
        /// </summary>
        /// <value>The default order.</value>
        [JsonPropertyName("default_order")]
        public int DefaultOrder { get; set; }

        /// <summary>
        /// Gets the child order.
        /// </summary>
        /// <value>The child order.</value>
        [JsonPropertyName("child_order")]
        public int ChildOrder { get; internal set; }

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

        /// <summary>
        /// Updates the related temporary ids.
        /// </summary>
        /// <param name="map">The map.</param>
        void IWithRelationsArgument.UpdateRelatedTempIds(IDictionary<Guid, string> map)
        {
            if (map.TryGetValue(WorkspaceId.TempId, out var persistentWorkspaceId))
            {
                WorkspaceId = new ComplexId(persistentWorkspaceId);
            }
        }
    }
}
