using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents an information about a Todoist email.
    /// </summary>
    public class TodoistObjectEmail
    {
        [JsonConstructor]
        internal TodoistObjectEmail()
        {
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [JsonPropertyName("email")]
        public string Email { get; internal set; }
    }
}
