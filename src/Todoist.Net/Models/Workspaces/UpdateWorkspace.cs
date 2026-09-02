using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a workspace payload for update requests.
    /// </summary>
    public class UpdateWorkspace : BaseWorkspace
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateWorkspace" /> class.
        /// </summary>
        /// <param name="id">The id of the workspace to update.</param>
        public UpdateWorkspace(ComplexId id)
            : base(id)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the workspace is collapsed for the current user.
        /// </summary>
        [JsonPropertyName("is_collapsed")]
        public bool? IsCollapsed { get; set; }

        /// <summary>
        /// Gets or sets a value that triggers invitation code regeneration when non-empty.
        /// </summary>
        [JsonPropertyName("invite_code")]
        public string InviteCode { get; set; }

    }
}
