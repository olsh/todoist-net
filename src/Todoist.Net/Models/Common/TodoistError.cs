using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the body of a command execution result.
    /// </summary>
    public class TodoistError
    {
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
        public TodoistErrorExtra ErrorExtra { get; internal set; }
    }

    /// <summary>
    /// Represents additional error information.
    /// </summary>
    public class TodoistErrorExtra
    {
        /// <summary>
        /// Gets the event identifier associated with the error.
        /// </summary>
        [JsonPropertyName("event_id")]
        public string EventId { get; internal set; }

        /// <summary>
        /// Gets the number of seconds to wait before retrying the request.
        /// </summary>
        [JsonPropertyName("retry_after")]
        public int? RetryAfter { get; internal set; }

        /// <summary>
        /// Gets the limit that was exceeded.
        /// </summary>
        [JsonPropertyName("max_count")]
        public int? MaxCount { get; internal set; }

        /// <summary>
        /// Gets the workspace ID related to the error.
        /// </summary>
        [JsonPropertyName("workspace_id")]
        public int? WorkspaceId { get; internal set; }

        /// <summary>
        /// Gets the project ID related to the error.
        /// </summary>
        [JsonPropertyName("project_id")]
        public string ProjectId { get; internal set; }

        /// <summary>
        /// Gets the section ID related to the error.
        /// </summary>
        [JsonPropertyName("section_id")]
        public string SectionId { get; internal set; }

        /// <summary>
        /// Gets the name of the argument that caused the error.
        /// </summary>
        [JsonPropertyName("argument")]
        public string Argument { get; internal set; }

        /// <summary>
        /// Gets the name of the command that caused the error.
        /// </summary>
        [JsonPropertyName("command")]
        public string Command { get; internal set; }

        /// <summary>
        /// Gets the expected value of the argument that caused the error.
        /// </summary>
        [JsonPropertyName("expected")]
        public string Expected { get; internal set; }

        /// <summary>
        /// Gets a detailed error description.
        /// </summary>
        [JsonPropertyName("explanation")]
        public string Explanation { get; internal set; }

        /// <summary>
        /// Gets information about the item that caused the error.
        /// </summary>
        [JsonPropertyName("bad_item")]
        public Dictionary<string, JsonElement> BadItem { get; internal set; }

        /// <summary>
        /// Gets any additional data that is not explicitly defined in the model.
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, JsonElement> ExtensionData { get; internal set; }
    }
}
