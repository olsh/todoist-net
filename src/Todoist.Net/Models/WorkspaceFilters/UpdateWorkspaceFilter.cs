using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents arguments for updating a workspace filter.
    /// </summary>
    public class UpdateWorkspaceFilter : BaseWorkspaceFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateWorkspaceFilter"/> class.
        /// </summary>
        /// <param name="id">The workspace filter identifier.</param>
        public UpdateWorkspaceFilter(ComplexId id)
            : base(id)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the filter is a favorite for the requesting user.
        /// </summary>
        [JsonPropertyName("is_favorite")]
        public bool? IsFavorite { get; set; }
    }
}
