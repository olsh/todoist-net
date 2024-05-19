using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a command execution error returned by the Todoist API.
    /// </summary>
    public class CommandError
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the error summary.
        /// </summary>
        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}
