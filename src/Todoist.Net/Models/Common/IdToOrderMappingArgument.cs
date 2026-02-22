using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents an argument for commands which require mapping of entity IDs to their new order.
    /// </summary>
    public class IdToOrderMappingArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdToOrderMappingArgument"/> class.
        /// </summary>
        /// <param name="ordersById">The mapping of entity IDs to their new order.</param>
        public IdToOrderMappingArgument(IDictionary<ComplexId, int> ordersById)
        {
            Ids = ordersById;
        }

        /// <summary>
        /// Gets the mapping of entity IDs to their new order.
        /// </summary>
        [JsonPropertyName("id_order_mapping")]
        public IDictionary<ComplexId, int> Ids { get; set; }
    }
}
