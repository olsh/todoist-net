using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a complete item argument.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.ICommandArgument" />
    public class CompleteItemArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteItemArgument" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="completedAt">
        /// The date completed. If not set, the server will set the value to the current timestamp.
        /// </param>
        public CompleteItemArgument(ComplexId id, DateTime? completedAt = null)
        {
            Id = id;
            CompletedAt = completedAt;
        }

        /// <summary>
        /// Gets the date completed.
        /// </summary>
        /// <value>
        /// The date completed.
        /// </value>
        [JsonPropertyName("completed_at")]
        public DateTime? CompletedAt { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonPropertyName("id")]
        public ComplexId Id { get; }
    }
}
