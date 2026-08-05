using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents arguments for updating a workspace filter.
    /// </summary>
    public class UpdateWorkspaceFilterOrders : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateWorkspaceFilterOrders"/> class.
        /// </summary>
        /// <param name="idOrderMapping">The workspace filter orders.</param>
        public UpdateWorkspaceFilterOrders(Dictionary<ComplexId, int> idOrderMapping)
        {
            IdOrderMapping = idOrderMapping;
        }

        /// <summary>
        /// Gets or sets the workspace filter orders.
        /// </summary>
        [JsonPropertyName("id_order_mapping")]
        public Dictionary<ComplexId, int> IdOrderMapping { get; set; }
    }
}
