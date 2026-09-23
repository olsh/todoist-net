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
        /// <param name="defaultOrderKey">The default order key.</param>
        public WorkspaceFolder(string name, string defaultOrderKey = null)
        {
            ThrowHelper.ThrowIfNullOrEmpty(name, nameof(name));

            Name = name;
            DefaultOrderKey = defaultOrderKey;
            AddProjectIds = new List<ComplexId>();
            RemoveProjectIds = new List<ComplexId>();
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
        /// Gets the fractional-indexing key for the workspace's shared default-ordering scope.
        /// </summary>
        /// <remarks>
        /// When omitted, the backend generates a key placing the folder at the bottom of that scope.
        /// If another project or folder in the workspace already uses the requested key, the backend stores an adjusted key placing the folder immediately after that position;
        /// the corrected value is returned via sync.
        /// </remarks>
        /// <value>The fractional-indexing key for the workspace's shared default-ordering scope.</value>
        [JsonPropertyName("default_order_key")]
        public string DefaultOrderKey { get; set; }

        /// <summary>
        /// Gets the legacy integer position within the workspace's shared default-ordering scope.
        /// </summary>
        /// <remarks>
        /// Superseded by <see cref="DefaultOrderKey"/>; when both are omitted the folder is placed at the bottom of the scope.
        /// </remarks>
        /// <value>The legacy integer position within the workspace's shared default-ordering scope.</value>
        [JsonPropertyName("default_order")]
        public int? DefaultOrder { get; set; }

        /// <summary>
        /// Gets the child order.
        /// </summary>
        /// <value>The child order.</value>
        [JsonPropertyName("child_order")]
        public int? ChildOrder { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the folder is deleted.
        /// </summary>
        /// <value>Indicates whether the folder is deleted.</value>
        [JsonPropertyName("is_deleted")]
        public bool? IsDeleted { get; internal set; }

        /// <summary>
        /// Gets the workspace ID.
        /// </summary>
        /// <value>The workspace ID.</value>
        [JsonPropertyName("workspace_id")]
        public ComplexId WorkspaceId { get; internal set; }

        /// <summary>
        /// Gets the workspace project IDs to move into the folder.
        /// </summary>
        /// <value>The workspace project IDs to move into the folder.</value>
        [JsonPropertyName("add_project_ids")]
        public ICollection<ComplexId> AddProjectIds { get; set; }

        /// <summary>
        /// Gets the workspace project IDs to move out of the folder.
        /// </summary>
        /// <value>The workspace project IDs to move out of the folder.</value>
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

            var mappedAddProjectIds = AddProjectIds
                .Join(map, addProjectId => addProjectId.TempId, kvp => kvp.Key, (addProjectId, kvp) => new
                {
                    Original = addProjectId,
                    Persistent = new ComplexId(kvp.Value)
                });
            foreach (var item in mappedAddProjectIds)
            {
                AddProjectIds.Remove(item.Original);
                AddProjectIds.Add(item.Persistent);
            }
            
            var mappedRemoveProjectIds = RemoveProjectIds
                .Join(map, removeProjectId => removeProjectId.TempId, kvp => kvp.Key, (removeProjectId, kvp) => new
                {
                    Original = removeProjectId,
                    Persistent = new ComplexId(kvp.Value)
                });
            foreach (var item in mappedRemoveProjectIds)
            {
                RemoveProjectIds.Remove(item.Original);
                RemoveProjectIds.Add(item.Persistent);
            }
        }
    }
}
