using System.Collections.Generic;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    internal class IdsToOrderIndentsArgument : ICommandArgument
    {
        public IdsToOrderIndentsArgument(IEnumerable<OrderIndentEntry> idsToOrderIndents)
        {
            IdsToOrderIndents = new Dictionary<ComplexId, IEnumerable<int>>();
            foreach (var entry in idsToOrderIndents)
            {
                IdsToOrderIndents.Add(
                    new KeyValuePair<ComplexId, IEnumerable<int>>(entry.Id, entry.ToArray()));
            }
        }

        [JsonProperty("ids_to_orders_indents")]
        public IDictionary<ComplexId, IEnumerable<int>> IdsToOrderIndents { get; set; }
    }
}
