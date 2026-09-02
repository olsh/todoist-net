using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the body of a command execution result.
    /// </summary>
    public class CommandResultBody : TodoistError
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
    }
}
