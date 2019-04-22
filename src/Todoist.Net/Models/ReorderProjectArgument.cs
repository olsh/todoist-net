using System.Collections.Generic;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    internal class ReorderProjectsArgument : ICommandArgument
    {
        public ReorderProjectsArgument(IEnumerable<ReorderEntry> reorderArguments)
        {
            ReorderArguments = reorderArguments;
        }

        [JsonProperty("projects")]
        public IEnumerable<ReorderEntry> ReorderArguments { get; }
    }
}
