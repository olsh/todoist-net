using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    internal class Invitation : ICommandArgument
    {
        [JsonConstructor]
        internal Invitation(string id, string secret = null)
        {
            InvitationId = id;
            InvitationSecret = secret;
        }

        [JsonPropertyName("invitation_id")]
        public string InvitationId { get; set; }

        [JsonPropertyName("invitation_secret")]
        public string InvitationSecret { get; set; }
    }
}
