using System;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist section.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.BaseEntity" />
    public class Section : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Section" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="sectionOrder">The section order.</param>
        public Section(string name, ComplexId projectId, int sectionOrder)
        {
            Name = name;
            ProjectId = projectId;
            SectionOrder = sectionOrder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Section" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="projectId">The project identifier.</param>
        public Section(string name, ComplexId projectId)
            : this(name, projectId, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Section"/> class.
        /// </summary>
        internal Section()
        {
        }

        /// <summary>
        /// Gets the date added.
        /// </summary>
        /// <value>
        /// The date when the section was created.
        /// </value>
        [JsonProperty("added_at")]
        public DateTime? AddedAt { get; internal set; }

        /// <summary>
        /// Gets the date archived.
        /// </summary>
        /// <value>
        /// The date when the section was archived (or null if not archived).
        /// </value>
        [JsonProperty("archived_at")]
        public DateTime? ArchivedAt { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is archived.
        /// </summary>
        /// <value><c>true</c> if this instance is archived; otherwise, <c>false</c>.</value>
        [JsonProperty("is_archived")]
        public bool IsArchived { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is collapsed.
        /// </summary>
        /// <value>
        /// <c>true</c> if the section’s tasks are collapsed; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("collapsed")]
        public bool IsCollapsed { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; internal set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name of the section..
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>
        /// Project that the section resides in.
        /// </value>
        [JsonProperty("project_id")]
        public ComplexId ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the section order.
        /// </summary>
        /// <value>
        /// The order of section. Defines the position of the section among all the sections in the project.
        /// </value>
        [JsonProperty("section_order")]
        public int SectionOrder { get; set; }
    }
}
