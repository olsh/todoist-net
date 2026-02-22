using System;
using System.Text.Json.Serialization;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a update workspace logo request payload.
    /// </summary>
    internal class WorkspaceLogoUpdateRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceLogoUpdateRequest"/> class.
        /// </summary>
        /// <param name="workspaceId">The workspace identifier.</param>
        /// <param name="fileContent">The file content.</param>
        /// <param name="delete">Indicates whether to delete the logo.</param>
        /// <exception cref="ArgumentNullException"><paramref name="fileContent"/> is <see langword="null"/>.</exception>
        public WorkspaceLogoUpdateRequest(long workspaceId, FileContent fileContent, bool delete = false)
        {
            ThrowHelper.ThrowIfNull(fileContent, nameof(fileContent));

            WorkspaceId = workspaceId;
            FileContent = fileContent;
            Delete = delete;
        }

        /// <summary>
        /// Gets or sets the workspace identifier.
        /// </summary>
        [JsonPropertyName("workspace_id")]
        public long WorkspaceId { get; set; }

        /// <summary>
        /// Gets or sets the file content.
        /// </summary>
        [JsonPropertyName("file_content")]
        public FileContent FileContent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to delete the logo.
        /// </summary>
        [JsonPropertyName("delete")]
        public bool Delete { get; set; }
    }
}
