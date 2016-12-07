using System.Collections.Generic;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    internal class UncompleteItemsArgument : ICommandArgument
    {
        public UncompleteItemsArgument(IEnumerable<ItemState> itemStates)
        {
            Ids = new LinkedList<ComplexId>();
            RestoreState = new Dictionary<ComplexId, int[]>();

            foreach (var state in itemStates)
            {
                Ids.Add(state.Id);
                RestoreState.Add(state.Id, state.ToArray());
            }
        }

        [JsonProperty("ids")]
        public ICollection<ComplexId> Ids { get; }

        [JsonProperty("restore_state")]
        public IDictionary<ComplexId, int[]> RestoreState { get; }
    }
}
