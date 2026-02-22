using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a reorder entry.
    /// </summary>
    public class ReorderArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReorderArgument" /> class.
        /// </summary>
        /// <param name="id">The identifier of moved entity.</param>
        /// <param name="childOrder">The new order.</param>
        /// <exception cref="T:System.ArgumentException">Entity ID is required for reorder operation</exception>
        public ReorderArgument(ComplexId id, int childOrder)
        {
            ThrowHelper.ThrowIfNullOrEmpty(id.ToString(), nameof(id));

            Id = id;
            ChildOrder = childOrder;
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        [JsonPropertyName("child_order")]
        public int ChildOrder { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonPropertyName("id")]
        public ComplexId Id { get; }


        /// <summary>
        /// Creates a new instance of the <see cref="ReorderArgument"/> class from the specified mapping of project IDs to their new order.
        /// </summary>
        /// <param name="ordersById">The mapping of project IDs to their new order.</param>
        /// <returns>A new instance of the <see cref="ReorderArgument"/> class.</returns>
        public static List<ReorderArgument> FromDictionary(IDictionary<ComplexId, int> ordersById)
        {
            return ordersById
                .Select(kvp => new ReorderArgument(kvp.Key, kvp.Value))
                .ToList();
        }
    }
}
