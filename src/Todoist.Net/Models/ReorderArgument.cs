using System;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a reorder entry.
    /// </summary>
    public class ReorderEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReorderEntry" /> class.
        /// </summary>
        /// <param name="id">The identifier of moved entity.</param>
        /// <param name="childOrder">The new order.</param>
        /// <exception cref="T:System.ArgumentException">Entity ID is required for reorder operation</exception>
        public ReorderEntry(ComplexId id, int childOrder)
        {
            if (id.IsEmpty)
            {
                throw new ArgumentException("Entity ID is required for reorder operation", nameof(id));
            }

            Id = id;
            ChildOrder = childOrder;
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        [JsonProperty("child_order")]
        public int ChildOrder { get; }

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
