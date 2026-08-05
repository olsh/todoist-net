using System;
using System.Text.Json.Serialization;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a delete workspace invitation request payload.
    /// </summary>
    internal class WorkspaceInvitationDeleteRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceInvitationDeleteRequest"/> class.
        /// </summary>
        /// <param name="workspaceId">The workspace identifier.</param>
        /// <param name="userEmail">The invited user email.</param>
        /// <exception cref="ArgumentException"><paramref name="userEmail"/> is null or empty.</exception>
        public WorkspaceInvitationDeleteRequest(long workspaceId, string userEmail)
        {
            ThrowHelper.ThrowIfNullOrEmpty(userEmail, nameof(userEmail));

            WorkspaceId = workspaceId;
            UserEmail = userEmail;
        }

        /// <summary>
        /// Gets or sets the workspace identifier.
        /// </summary>
        [JsonPropertyName("workspace_id")]
        public long WorkspaceId { get; set; }

        /// <summary>
        /// Gets or sets the invited user email.
        /// </summary>
        [JsonPropertyName("user_email")]
        public string UserEmail { get; set; }
    }
}
