using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a response that contains synchronized collaborators data.
    /// </summary>
    public class CollaboratorsSyncResponse : BaseSyncResponse
    {
        /// <summary>Gets the collaborators.</summary>
        [JsonPropertyName("collaborators")]
        public IReadOnlyCollection<Collaborator> Collaborators { get; internal set; }

        /// <summary>Gets the collaborator states.</summary>
        [JsonPropertyName("collaborator_states")]
        public IReadOnlyCollection<CollaboratorState> CollaboratorStates { get; internal set; }
    }
}
