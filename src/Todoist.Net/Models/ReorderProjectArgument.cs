using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    internal class ReorderProjectsArgument : ICommandArgument
    {
        public ReorderProjectsArgument(IEnumerable<ReorderEntry> reorderArguments)
        {
            ReorderArguments = reorderArguments;
        }

        [JsonPropertyName("projects")]
        public IEnumerable<ReorderEntry> ReorderArguments { get; }
    }
}
