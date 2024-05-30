using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    internal class ProjectReorderArgument : ICommandArgument
    {
        public ProjectReorderArgument(IEnumerable<ReorderEntry> reorderEntries)
        {
            ReorderEntries = reorderEntries;
        }

        [JsonPropertyName("projects")]
        public IEnumerable<ReorderEntry> ReorderEntries { get; }
    }
}
