using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents arguments for reordering tasks.
    /// </summary>
    public class ReorderTasksArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReorderTasksArgument" /> class
        /// with the specified reorder arguments for the tasks.
        /// </summary>
        /// <param name="reorderArguments">The reorder arguments for the tasks.</param>
        public ReorderTasksArgument(params ReorderArgument[] reorderArguments)
        {
            ReorderArguments = reorderArguments.ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReorderTasksArgument"/> class from
        /// the specified mapping of task IDs to their new order.
        /// </summary>
        /// <param name="ordersById">The mapping of task IDs to their new order.</param>
        public ReorderTasksArgument(IDictionary<ComplexId, int> ordersById)
        {
            ReorderArguments = ReorderArgument.FromDictionary(ordersById);
        }

        /// <remarks>
        /// The JSON property name remains "items" for backwards compatibility with Sync API.
        /// </remarks>
        [JsonPropertyName("items")]
        public ICollection<ReorderArgument> ReorderArguments { get; }
    }
}
