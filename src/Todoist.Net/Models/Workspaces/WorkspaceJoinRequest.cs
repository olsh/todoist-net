using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a join-workspace request.
    /// </summary>
    internal class WorkspaceJoinRequest
    {
        /// <summary>
        /// Gets or sets the invitation code.
        /// </summary>
        [JsonPropertyName("invite_code")]
        public string InviteCode { get; set; }

        /// <summary>
        /// Gets or sets the workspace identifier for auto-join by domain.
        /// </summary>
        [JsonPropertyName("workspace_id")]
        public long? WorkspaceId { get; set; }
    }
}
