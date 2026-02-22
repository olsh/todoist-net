using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    internal class TemplateImportRequest
    {
        [JsonConstructor]
        internal TemplateImportRequest()
        {
        }

        /// <summary>
        /// Gets or sets the project ID.
        /// </summary>
        [JsonPropertyName("project_id")]
        public string ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the template ID.
        /// </summary>
        [JsonPropertyName("template_id")]
        public string TemplateId { get; set; }
    }
}
