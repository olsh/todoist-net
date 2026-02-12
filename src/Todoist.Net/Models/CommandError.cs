#if NETFRAMEWORK
using System;
#endif

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a command execution error returned by the Todoist API.
    /// </summary>
#if NETFRAMEWORK
    [Serializable]
#endif
    public class CommandError
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the error summary.
        /// </summary>
        /// <value>The error message.</value>
        [JsonPropertyName("error")]
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the error tag.
        /// </summary>
        /// <value>The error tag (e.g., "NOT_FOUND", "INVALID_ARGUMENT_VALUE").</value>
        [JsonPropertyName("error_tag")]
        public string ErrorTag { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        /// <value>The HTTP status code.</value>
        [JsonPropertyName("http_code")]
        public int HttpCode { get; set; }

        /// <summary>
        /// Gets or sets the extra error information.
        /// </summary>
        /// <value>A dictionary containing additional error details (e.g., "event_id", "retry_after").</value>
        [JsonPropertyName("error_extra")]
        public Dictionary<string, object> ErrorExtra { get; set; }
    }
}
