using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents arguments for reordering sections.
    /// </summary>
    public class ReorderSectionsArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReorderSectionsArgument" /> class.
        /// </summary>
        /// <param name="sectionOrders">The reorder arguments for the sections.</param>
        /// <exception cref="T:System.ArgumentException">Entity ID is required for the operation</exception>
        public ReorderSectionsArgument(params SectionReorderArgument[] sectionOrders)
        {
            SectionOrders = sectionOrders.ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReorderSectionsArgument"/> class from
        /// the specified mapping of section IDs to their new order.
        /// </summary>
        /// <param name="ordersById">The mapping of section IDs to their new order.</param>
        public ReorderSectionsArgument(IDictionary<ComplexId, int> ordersById)
        {
            SectionOrders = ordersById
                .Select(kvp => new SectionReorderArgument(kvp.Key, kvp.Value))
                .ToList();
        }

        /// <summary>
        /// Gets the reorder arguments for the sections.
        /// </summary>
        [JsonPropertyName("sections")]
        public ICollection<SectionReorderArgument> SectionOrders { get; }
    }
}
