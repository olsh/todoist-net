using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    internal class ObjectEmailRequest
    {
        [JsonConstructor]
        internal ObjectEmailRequest()
        {
        }

        /// <summary>
        /// Gets or sets the email object type.
        /// </summary>
        [JsonPropertyName("obj_type")]
        public EmailObjectType ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the email object ID.
        /// </summary>
        [JsonPropertyName("obj_id")]
        public string ObjectId { get; set; }
    }
}
