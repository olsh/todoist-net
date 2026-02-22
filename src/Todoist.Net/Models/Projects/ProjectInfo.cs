using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents full project details returned by get requests.
    /// </summary>
    public class ProjectInfo : AddProject
    {
        /// <summary>
        /// Gets a value indicating whether subprojects are collapsed.
        /// </summary>
        [JsonPropertyName("is_collapsed")]
        public bool? IsCollapsed { get; set; }
        
        /// <summary>
        /// Gets a value indicating whether this project is archived.
        /// </summary>
        [JsonPropertyName("is_archived")]
        public bool? IsArchived { get; set; }

        /// <summary>
        /// Gets a value indicating whether the project is deleted.
        /// </summary>
        [JsonPropertyName("is_deleted")]
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// Gets a value indicating whether the project is shared.
        /// </summary>
        [JsonPropertyName("is_shared")]
        public bool? IsShared { get; set; }

        /// <summary>
        /// Gets a value indicating whether project is frozen.
        /// </summary>
        [JsonPropertyName("is_frozen")]
        public bool? IsFrozen { get; set; }

        /// <summary>
        /// Gets a value indicating whether project insights are enabled.
        /// </summary>
        [JsonPropertyName("is_project_insights_enabled")]
        public bool? IsProjectInsightsEnabled { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether pending default collaborator invites exist.
        /// </summary>
        [JsonPropertyName("is_pending_default_collaborator_invites")]
        public bool? IsPendingDefaultCollaboratorInvites { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether tasks can be assigned in this project.
        /// </summary>
        [JsonPropertyName("can_assign_tasks")]
        public bool? CanAssignTasks { get; set; }

        /// <summary>
        /// Gets a value indicating whether the project is an inbox project.
        /// </summary>
        [JsonPropertyName("inbox_project")]
        public bool? InboxProject { get; set; }

        /// <summary>
        /// Gets a value indicating whether public access is enabled.
        /// </summary>
        [JsonPropertyName("public_access")]
        public bool? PublicAccess { get; set; }

        /// <summary>
        /// Gets the default order.
        /// </summary>
        [JsonPropertyName("default_order")]
        public int? DefaultOrder { get; set; }

        /// <summary>
        /// Gets the user role in the project.
        /// </summary>
        [JsonPropertyName("role")]
        public ProjectCollaboratorRole Role { get; set; }

        /// <summary>
        /// Gets the project creation timestamp.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets the project update timestamp.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets the creator user id.
        /// </summary>
        [JsonPropertyName("creator_uid")]
        public string CreatorUid { get; set; }

        /// <summary>
        /// Gets the project public key.
        /// </summary>
        [JsonPropertyName("public_key")]
        public string PublicKey { get; set; }
    }
}
