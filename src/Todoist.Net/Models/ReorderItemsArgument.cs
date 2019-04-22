using System.Collections.Generic;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    internal class ReorderItemsArgument : ICommandArgument
    {
        public ReorderItemsArgument(IEnumerable<ReorderEntry> reorderArguments)
        {
            ReorderArguments = reorderArguments;
        }

        [JsonProperty("items")]
        public IEnumerable<ReorderEntry> ReorderArguments { get; }
    }
}
