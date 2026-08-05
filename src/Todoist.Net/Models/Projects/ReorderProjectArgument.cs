using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents arguments for reordering projects.
    /// </summary>
    public class ReorderProjectsArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReorderProjectsArgument" /> class.
        /// </summary>
        /// <param name="projectOrders">The reorder arguments for the projects.</param>
        /// <exception cref="T:System.ArgumentException">Entity ID is required for the operation</exception>
        public ReorderProjectsArgument(params ReorderArgument[] projectOrders)
        {
            ProjectOrders = projectOrders.ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReorderProjectsArgument"/> class from
        /// the specified mapping of project IDs to their new order.
        /// </summary>
        /// <param name="ordersById">The mapping of project IDs to their new order.</param>
        public ReorderProjectsArgument(IDictionary<ComplexId, int> ordersById)
        {
            ProjectOrders = ReorderArgument.FromDictionary(ordersById);
        }

        /// <summary>
        /// Gets the reorder arguments for the projects.
        /// </summary>
        [JsonPropertyName("projects")]
        public ICollection<ReorderArgument> ProjectOrders { get; }
    }
}
