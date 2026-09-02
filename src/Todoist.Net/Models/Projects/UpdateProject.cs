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
    }
}
