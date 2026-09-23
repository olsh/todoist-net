using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <remarks>
        /// Superseded by <see cref="DefaultOrderKey" />: Todoist renumbers the default order of a workspace's projects and folders
        /// to follow their keys, so the value sent when adding a folder doesn't decide where it goes.
        /// </remarks>
        /// <value>The default order.</value>
        [JsonPropertyName("default_order")]
        public int DefaultOrder { get; set; }

        /// <summary>
        /// Gets the child order.
        /// </summary>
        /// <value>The child order.</value>
        [JsonPropertyName("child_order")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
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
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool IsDeleted { get; internal set; }

        /// <summary>
        /// Gets or sets the fractional-indexing key which orders the folder among the workspace's projects and folders.
        /// </summary>
        /// <remarks>
        /// Keys sort as ordinal strings. A folder added without a key goes to the bottom, and an update without one keeps the current key.
        /// If another project or folder already uses the key, Todoist stores a key right after it instead, which a sync returns.
        /// Todoist returns <c>null</c> for folders it hasn't assigned a key yet.
        /// </remarks>
        /// <value>The default order key.</value>
        [JsonPropertyName("default_order_key")]
        public string DefaultOrderKey { get; set; }

        /// <summary>
        /// Gets or sets the IDs of workspace projects to move into the folder when it's added or updated.
        /// </summary>
        /// <value>The IDs of the projects to move into the folder.</value>
        [JsonPropertyName("add_project_ids")]
        public ICollection<ComplexId> AddProjectIds { get; set; }

        /// <summary>
        /// Gets or sets the IDs of workspace projects to move out of the folder when it's updated.
        /// </summary>
        /// <value>The IDs of the projects to move out of the folder.</value>
        [JsonPropertyName("remove_project_ids")]
        public ICollection<ComplexId> RemoveProjectIds { get; set; }

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

            AddProjectIds = ReplaceTempIds(AddProjectIds, map);
            RemoveProjectIds = ReplaceTempIds(RemoveProjectIds, map);
        }

        private static ICollection<ComplexId> ReplaceTempIds(ICollection<ComplexId> ids, IDictionary<Guid, string> map)
        {
            // The collection is replaced rather than changed in place, because the caller may have passed a read-only one, such as an array.
            if (ids == null || !ids.Any(id => map.ContainsKey(id.TempId)))
            {
                return ids;
            }

            return ids
                .Select(id => map.TryGetValue(id.TempId, out var persistentId) ? new ComplexId(persistentId) : id)
                .ToList();
        }
    }
}
