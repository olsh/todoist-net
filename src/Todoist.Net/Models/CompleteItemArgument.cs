using System;

using Newtonsoft.Json;

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
        /// <param name="dateCompleted">
        /// The date completed. If not set, the server will set the value to the current timestamp.
        /// </param>
        /// <param name="forceHistory">
        /// The force history. When enabled the item is moved to history irregardless of whether it’s a
        /// sub-task or not (by default only root tasks are moved to history).
        /// </param>
        public CompleteItemArgument(ComplexId id, DateTime? dateCompleted = null, bool? forceHistory = null)
        {
            Id = id;
            DateCompleted = dateCompleted;
            ForceHistory = forceHistory;
        }

        /// <summary>
        /// Gets the date completed.
        /// </summary>
        /// <value>
        /// The date completed.
        /// </value>
        [JsonProperty("date_completed")]
        public DateTime? DateCompleted { get; }

        /// <summary>
        /// Gets the force history.
        /// </summary>
        /// <value>
        /// The force history.
        /// </value>
        [JsonProperty("force_history")]
        public bool? ForceHistory { get; }

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
