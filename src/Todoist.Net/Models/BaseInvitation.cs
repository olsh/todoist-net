using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a base invitation.
    /// </summary>
    internal class BaseInvitation : ICommandArgument
    {
        [JsonConstructor]
        internal BaseInvitation(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonPropertyName("invitation_id")]
        public long Id { get; set; }
    }
}
