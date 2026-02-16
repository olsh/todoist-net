using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    internal class ReorderTasksArgument : ICommandArgument
    {
        public ReorderTasksArgument(IEnumerable<ReorderArgument> reorderArguments)
        {
            ReorderArguments = reorderArguments;
        }

        /// <remarks>
        /// The JSON property name remains "items" for backwards compatibility with Sync API.
        /// </remarks>
        [JsonPropertyName("items")]
        public IEnumerable<ReorderArgument> ReorderArguments { get; }
    }
}
