using System.Collections.Generic;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    internal class CompleteItemsArgument : ICommandArgument
    {
        public CompleteItemsArgument(IEnumerable<ComplexId> ids, bool forceHistory)
        {
            Ids = ids;
            ForceHistory = forceHistory;
        }

        [JsonProperty("force_history")]
        public bool ForceHistory { get; }

        [JsonProperty("ids")]
        public IEnumerable<ComplexId> Ids { get; }
    }
}
