using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the result of a command long-running operation.
    /// </summary>
    public class CommandOperation
    {
        /// <summary>
        /// Gets the operation id.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; internal set; }

        /// <summary>
        /// Gets the operation type.
        /// </summary>
        [JsonPropertyName("operation_type")]
        public string OperationType { get; internal set; }
        
        /// <summary>
        /// Gets the operation status.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; internal set; }
        
        /// <summary>
        /// Gets the error message if the operation failed.
        /// </summary>
        [JsonPropertyName("error")]
        public string Error { get; internal set; }
        
        /// <summary>
        /// Gets the date and time when the operation was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; internal set; }
        
        /// <summary>
        /// Gets the date and time when the operation was last updated.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; internal set; }

        /// <summary>
        /// Gets the command arguments.
        /// </summary>
        [JsonPropertyName("args")]
        public Dictionary<string, object> Args { get; internal set; }
    }
}
