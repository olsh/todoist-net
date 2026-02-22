using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the body of a command execution result.
    /// </summary>
    public class CommandResultBody
    {
        /// <summary>        
        /// Gets a value indicating whether the command execution resulted in an error.
        /// </summary>
        public bool IsError => Error != null || Operation == null;

        /// <summary>
        /// Gets the command operation result.
        /// </summary>
        [JsonPropertyName("operation")]
        public CommandOperation Operation { get; internal set; }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        [JsonPropertyName("error_code")]
        public int? ErrorCode { get; internal set; }

        /// <summary>
        /// Gets the error summary.
        /// </summary>
        [JsonPropertyName("error")]
        public string Error { get; internal set; }

        /// <summary>
        /// Gets the error tag.
        /// </summary>
        /// <value>The error tag (e.g., "NOT_FOUND", "INVALID_ARGUMENT_VALUE").</value>
        [JsonPropertyName("error_tag")]
        public string ErrorTag { get; internal set; }

        /// <summary>
        /// Gets the HTTP status code.
        /// </summary>
        [JsonPropertyName("http_code")]
        public int? HttpCode { get; internal set; }

        /// <summary>
        /// Gets the extra error information.
        /// </summary>
        /// <value>A dictionary containing additional error details (e.g., "event_id", "retry_after").</value>
        [JsonPropertyName("error_extra")]
        public Dictionary<string, object> ErrorExtra { get; internal set; }
    }
}
