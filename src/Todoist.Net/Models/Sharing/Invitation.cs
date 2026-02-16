using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    internal class Invitation : BaseInvitation
    {
        [JsonConstructor]
        internal Invitation(long id, string secret)
            : base(id)
        {
            Secret = secret;
        }

        [JsonPropertyName("invitation_secret")]
        public string Secret { get; set; }
    }
}
