using System.Collections.Generic;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    internal class IdToOrderArgument : ICommandArgument
    {
        public IdToOrderArgument(IEnumerable<OrderEntry> orderEntries)
        {
            Ids = new Dictionary<ComplexId, int>();
            foreach (var entry in orderEntries)
            {
                Ids.Add(entry.Id, entry.Order);
            }
        }

        [JsonProperty("ids_to_orders")]
        public IDictionary<ComplexId, int> Ids { get; set; }
    }
}
