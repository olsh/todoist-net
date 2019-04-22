using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents complete item argument.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.ICommandArgument" />
    public class CompleteRecurringItemArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteRecurringItemArgument" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dueDate">The due date.</param>
        public CompleteRecurringItemArgument(ComplexId id, DueDate dueDate = null)
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
        [JsonProperty("due")]
        public DueDate DueDate { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("id")]
        public ComplexId Id { get; }
    }
}
