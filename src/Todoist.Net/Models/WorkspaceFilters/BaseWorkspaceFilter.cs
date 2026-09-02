using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents common workspace filter data used by add, update, and read operations.
    /// </summary>
    public class BaseWorkspaceFilter : BaseUnsetEntity
    {
        private protected BaseWorkspaceFilter(ComplexId id)
            : base(id)
        {
        }

        [JsonConstructor]
        private protected BaseWorkspaceFilter()
        {
        }

        /// <summary>
        /// Gets or sets the name of the workspace filter.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the query to search for.
        /// </summary>
        [JsonPropertyName("query")]
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the color of the filter icon.
        /// </summary>
        [JsonPropertyName("color")]
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the filter order in the filter list.
        /// </summary>
        [JsonPropertyName("item_order")]
        public int? ItemOrder { get; set; }
    }
}
