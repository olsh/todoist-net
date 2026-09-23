using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a project payload for update requests.
    /// </summary>
    public class UpdateProject : BaseProject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProject" /> class.
        /// </summary>
        /// <param name="id">The id of the project to update.</param>
        public UpdateProject(ComplexId id)
            : base(id)
        {
        }

        /// <summary>
        /// Gets a value indicating whether subprojects are collapsed.
        /// </summary>
        [JsonPropertyName("is_collapsed")]
        public bool? IsCollapsed { get; set; }

        /// <summary>
        /// Gets or sets the fractional-indexing key which orders a workspace project among the workspace's projects and folders.
        /// </summary>
        /// <remarks>
        /// Todoist ignores it for personal projects. Keys sort as ordinal strings; without a key, the project keeps its current one.
        /// If another project or folder already uses the key, Todoist stores a key right after it instead, which a sync returns.
        /// </remarks>
        /// <value>The default order key.</value>
        [JsonPropertyName("default_order_key")]
        public string DefaultOrderKey { get; set; }
    }
}
