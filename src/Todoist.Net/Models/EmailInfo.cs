using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents an information about a Todoist email.
    /// </summary>
    public class EmailInfo
    {
        internal EmailInfo()
        {
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [JsonProperty("email")]
        public string Email { get; internal set; }
    }
}
