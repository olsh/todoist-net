using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a complete task argument.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.ICommandArgument" />
    public class CompleteTaskArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteTaskArgument" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dateCompleted">
        /// The date completed. If not set, the server will set the value to the current timestamp.
        /// </param>
        /// <param name="fromUndo">
        /// If <c>true</c>, skips incrementing completion stats. Used when restoring task state after undoing a completion.
        /// </param>
        public CompleteTaskArgument(ComplexId id, DateTime? dateCompleted = null, bool? fromUndo = null)
        {
            Id = id;
            DateCompleted = dateCompleted;
            FromUndo = fromUndo;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonPropertyName("id")]
        public ComplexId Id { get; }

        /// <summary>
        /// Gets the date completed.
        /// </summary>
        /// <value>
        /// The date completed.
        /// </value>
        [JsonPropertyName("date_completed")]
        public DateTime? DateCompleted { get; }

        /// <summary>
        /// Gets a value indicating whether the completion stats should be skipped.
        /// </summary>
        /// <value>
        /// If <c>true</c>, skips incrementing completion stats. Used when restoring task state after undoing a completion.
        /// </value>
        [JsonPropertyName("from_undo")]
        public bool? FromUndo { get; }
    }
}
