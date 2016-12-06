using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    internal class MoveItemArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoveItemArgument"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <exception cref="ArgumentException">Unable to move an item with an empty project ID.</exception>
        public MoveItemArgument(IEnumerable<Item> items, ComplexId projectId)
        {
            ProjectItems = new Dictionary<ComplexId, ICollection<ComplexId>>();
            foreach (var item in items)
            {
                if (!item.ProjectId.HasValue)
                {
                    throw new ArgumentException("Unable to move item with an empty project ID.", nameof(items));
                }

                ICollection<ComplexId> itemIds;
                if (!ProjectItems.TryGetValue(item.ProjectId.Value, out itemIds))
                {
                    itemIds = new LinkedList<ComplexId>();
                    ProjectItems.Add(item.ProjectId.Value, itemIds);
                }

                itemIds.Add(item.Id);
            }

            ToProject = projectId;
        }

        [JsonProperty("project_items")]
        public IDictionary<ComplexId, ICollection<ComplexId>> ProjectItems { get; }

        [JsonProperty("to_project")]
        public ComplexId ToProject { get; }
    }
}
