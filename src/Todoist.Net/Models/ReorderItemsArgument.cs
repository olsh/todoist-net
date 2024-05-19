using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    internal class ReorderItemsArgument : ICommandArgument
    {
        public ReorderItemsArgument(IEnumerable<ReorderEntry> reorderArguments)
        {
            ReorderArguments = reorderArguments;
        }

        [JsonPropertyName("items")]
        public IEnumerable<ReorderEntry> ReorderArguments { get; }
    }
}
