using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist task.
    /// </summary>
    public class AddItem : BaseItem, IWithRelationsArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddItem" /> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public AddItem(string content)

            // ReSharper disable once IntroduceOptionalParameters.Global
            : this(content, default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddItem" /> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="projectId">The project identifier.</param>
        public AddItem(string content, ComplexId projectId)
        {
            Content = content;
            ProjectId = projectId;
            Labels = new Collection<string>();
        }


        /// <summary>
        /// Gets or sets order of project. Defines the position of the project among all the projects with the same parent_id.
        /// </summary>
        /// <value>The project order.</value>
        [JsonPropertyName("child_order")]
        public int? ChildOrder { get; set; }

        /// <summary>
        /// Gets or sets the id of the parent task. Set to <see langword="null" /> for root tasks.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        [JsonPropertyName("parent_id")]
        public string ParentId { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>The project identifier.</value>
        [JsonPropertyName("project_id")]
        public ComplexId? ProjectId { get; set; }

        /// <summary>
        /// Gets or sets section of project. Defines the section that the task belongs to.
        /// </summary>
        /// <value>The project order.</value>
        [JsonPropertyName("section_id")]
        public string Section { get; set; }

        /// <summary>
        /// Gets a value indicating whether to add the default reminder to the new item if it has a due date.
        /// </summary>
        /// <value>
        /// <c>null</c> if [auto reminder] contains no value, <c>true</c> to add the default reminder. Defaults to <c>false</c>.
        /// </value>
        [JsonPropertyName("auto_reminder")]
        public bool? AutoReminder { get; set; }

        /// <summary>
        /// Gets a value indicating whether the labels should be parsed from the task content.
        /// </summary>
        /// <value>
        /// <c>null</c> if [auto parse labels] contains no value, <c>true</c> to parse labels from content. Defaults to <c>false</c>.
        /// </value>
        [JsonPropertyName("auto_parse_labels")]
        public bool? AutoParseLabels { get; set; }


        /// <summary>
        /// Updates the related temporary ids.
        /// </summary>
        /// <param name="map">The map.</param>
        void IWithRelationsArgument.UpdateRelatedTempIds(IDictionary<Guid, string> map)
        {
            if (ProjectId.HasValue && map.TryGetValue(ProjectId.Value.TempId, out var persistentProjectId))
            {
                ProjectId = new ComplexId(persistentProjectId);
            }
        }
    }
}
