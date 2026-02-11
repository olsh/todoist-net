using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents complete recurring task argument.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.ICommandArgument" />
    public class CompleteRecurringTaskArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteRecurringTaskArgument" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dueDate">The due date.</param>
        public CompleteRecurringTaskArgument(ComplexId id, DueDate dueDate = null)
        {
            DueDate = dueDate;
            Id = id;
        }

        /// <summary>
        /// Gets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        [JsonPropertyName("due")]
        public DueDate DueDate { get; }

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
