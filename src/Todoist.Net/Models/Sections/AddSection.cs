using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist section.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.BaseEntity" />
    public class AddSection : BaseEntity, IWithRelationsArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddSection" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="sectionOrder">The section order.</param>
        public AddSection(string name, ComplexId projectId, int? sectionOrder = null)
        {
            Name = name;
            ProjectId = projectId;
            SectionOrder = sectionOrder;
        }
        
        [JsonConstructor]
        internal AddSection()
        {
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name of the section.
        /// </value>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>
        /// Project that the section resides in.
        /// </value>
        [JsonPropertyName("project_id")]
        public ComplexId ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the section order.
        /// </summary>
        /// <value>
        /// The order of section. Defines the position of the section among all the sections in the project.
        /// </value>
        [JsonPropertyName("section_order")]
        public int? SectionOrder { get; set; }

        /// <remarks>
        /// Required for `section_order` alias in the REST API (`order`).
        /// </remarks>
        [JsonInclude]
        [JsonPropertyName("order")]
        internal int? Order => SectionOrder;

        
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
