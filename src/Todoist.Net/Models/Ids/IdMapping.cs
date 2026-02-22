using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a v1/v2 identifier mapping.
    /// </summary>
    public class IdMapping
    {
        /// <summary>
        /// Gets or sets the old identifier.
        /// </summary>
        [JsonPropertyName("old_id")]
        public string OldId { get; set; }

        /// <summary>
        /// Gets or sets the new identifier.
        /// </summary>
        [JsonPropertyName("new_id")]
        public string NewId { get; set; }
    }
}
