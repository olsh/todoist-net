using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents arguments for adding a workspace filter.
    /// </summary>
    public class AddWorkspaceFilter : BaseWorkspaceFilter, IWithRelationsArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddWorkspaceFilter"/> class.
        /// </summary>
        /// <param name="workspaceId">The workspace identifier.</param>
        /// <param name="name">The filter name.</param>
        /// <param name="query">The filter query.</param>
        /// <exception cref="ArgumentException">Thrown when name or query is null or empty.</exception>
        public AddWorkspaceFilter(ComplexId workspaceId, string name, string query)
        {
            ThrowHelper.ThrowIfNullOrEmpty(name, nameof(name));
            ThrowHelper.ThrowIfNullOrEmpty(query, nameof(query));

            WorkspaceId = workspaceId;
            Name = name;
            Query = query;
        }

        /// <summary>
        /// Gets or sets the ID of the workspace this filter belongs to.
        /// </summary>
        [JsonPropertyName("workspace_id")]
        public ComplexId WorkspaceId { get; set; }

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
