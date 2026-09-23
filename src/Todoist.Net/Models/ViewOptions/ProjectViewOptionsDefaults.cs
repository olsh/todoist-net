using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a project view options default values. 
    /// </summary>
    public class ProjectViewOptionsDefaults : BaseViewOptions, IWithRelationsArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectViewOptionsDefaults"/> class.
        /// </summary>
        /// <param name="projectId">The project ID.</param>
        public ProjectViewOptionsDefaults(ComplexId projectId)
        {
            ProjectId = projectId;
        }

        [JsonConstructor]
        internal ProjectViewOptionsDefaults()
        {
        }

        /// <summary>
        /// Gets or sets the project ID.
        /// </summary>
        [JsonPropertyName("project_id")]
        public ComplexId ProjectId { get; set; }

        /// <summary>
        /// Gets the creator user ID.
        /// </summary>
        [JsonPropertyName("creator_uid")]
        public long? CreatorUid { get; internal set; }

        /// <summary>
        /// Gets the updater user ID.
        /// </summary>
        [JsonPropertyName("updater_uid")]
        public long? UpdaterUid { get; internal set; }


        /// <summary>
        /// Updates the related temporary ids.
        /// </summary>
        /// <param name="map">The map.</param>
        void IWithRelationsArgument.UpdateRelatedTempIds(IDictionary<Guid, string> map)
        {
            if (map.TryGetValue(ProjectId.TempId, out var persistentProjectId))
            {
                ProjectId = new ComplexId(persistentProjectId);
            }
        }
    }
}
