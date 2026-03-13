using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a template import/create-project result.
    /// </summary>
    public class TemplateImportResult
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the template type.
        /// </summary>
        [JsonPropertyName("template_type")]
        public string TemplateType { get; set; }

        /// <summary>
        /// Gets or sets the created project identifier when available.
        /// </summary>
        [JsonPropertyName("project_id")]
        public string ProjectId { get; set; }

        /// <summary>Gets the projects.</summary>
        [JsonPropertyName("projects")]
        public IReadOnlyCollection<ProjectInfo> Projects { get; internal set; }

        /// <summary>Gets the sections.</summary>
        [JsonPropertyName("sections")]
        public IReadOnlyCollection<Section> Sections { get; internal set; }

        /// <summary>Gets the tasks.</summary>
        [JsonPropertyName("tasks")]
        public IReadOnlyCollection<TaskInfo> Tasks { get; internal set; }

        /// <summary>Gets the comments.</summary>
        [JsonPropertyName("comments")]
        public IReadOnlyCollection<Comment> Comments { get; internal set; }

        /// <summary>Gets the project comments.</summary>
        /// <remarks>The JSON property name remains "project_notes" for backwards compatibility with Sync API.</remarks>
        [JsonPropertyName("project_notes")]
        public IReadOnlyCollection<Comment> ProjectComments { get; internal set; }
    }
}
