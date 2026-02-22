using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist section.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.BaseEntity" />
    public class Section : AddSection
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
        /// Initializes a new instance of the <see cref="Section"/> class.
        /// </summary>
        [JsonConstructor]
        internal Section()
        {
        }

        /// <summary>
        /// Gets the date added.
        /// </summary>
        /// <value>
        /// The date when the section was created.
        /// </value>
        [JsonPropertyName("added_at")]
        public DateTime? AddedAt { get; internal set; }

        /// <summary>
        /// Gets the date updated.
        /// </summary>
        /// <value>
        /// The date when the section was last updated.
        /// </value>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; internal set; }

        /// <summary>
        /// Gets the date archived.
        /// </summary>
        /// <value>
        /// The date when the section was archived (or null if not archived).
        /// </value>
        [JsonPropertyName("archived_at")]
        public DateTime? ArchivedAt { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is archived.
        /// </summary>
        /// <value><c>true</c> if this instance is archived; otherwise, <c>false</c>.</value>
        [JsonPropertyName("is_archived")]
        public bool IsArchived { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is collapsed.
        /// </summary>
        /// <value>
        /// <c>true</c> if the section’s tasks are collapsed; otherwise, <c>false</c>.
        /// </value>
        [JsonPropertyName("collapsed")]
        public bool IsCollapsed { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; internal set; }

        /// <summary>
        /// Gets a special ID for shared sections.
        /// </summary>
        /// <value>
        /// The special ID for shared sections.
        /// </value>
        [JsonPropertyName("sync_id")]
        public string SyncId { get; internal set; }
    }
}
