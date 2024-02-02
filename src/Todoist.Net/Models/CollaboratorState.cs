using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist collaborator state.
    /// </summary>
    public class CollaboratorState
    {
        internal CollaboratorState()
        {
        }

        /// <summary>
        /// Gets the user id of the collaborator.
        /// </summary>
        /// <value>The user id.</value>
        [JsonProperty("user_id")]
        public string UserId { get; internal set; }

        /// <summary>
        /// Gets the shared project id.
        /// </summary>
        /// <value>The project id.</value>
        [JsonProperty("project_id")]
        public string ProjectId { get; internal set; }

        /// <summary>
        /// Gets the collaborator status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty("state")]
        public CollaboratorStatus State { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this collaborator is deleted.
        /// </summary>
        /// <value><c>true</c> if this collaborator is deleted; otherwise, <c>false</c>.</value>
        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; internal set; }

    }
}
