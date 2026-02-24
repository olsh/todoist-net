using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a project payload for add requests.
    /// </summary>
    public class AddProject : BaseProject
    {
        internal AddProject()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddProject" /> class.
        /// </summary>
        /// <param name="name">The project name.</param>
        public AddProject(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets the parent project identifier.
        /// </summary>
        /// <value>The parent project identifier.</value>
        [JsonPropertyName("parent_id")]
        public ComplexId? ParentId { get; set; }

        /// <summary>
        /// Gets the folder id.
        /// </summary>
        [JsonPropertyName("folder_id")]
        public ComplexId? FolderId { get; set; }

        /// <summary>
        /// Gets the workspace id.
        /// </summary>
        [JsonPropertyName("workspace_id")]
        public ComplexId? WorkspaceId { get; set; }

        /// <summary>
        /// Gets or sets order of project. Defines the position of the project among all the projects with the same parent_id.
        /// </summary>
        /// <value>The project order.</value>
        [JsonPropertyName("child_order")]
        public int? ChildOrder { get; set; }

        /// <summary>
        /// Gets a value indicating whether project is invite-only.
        /// </summary>
        [JsonPropertyName("is_invite_only")]
        public bool? IsInviteOnly { get; set; }
    }
}
