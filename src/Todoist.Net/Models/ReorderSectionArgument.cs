using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    internal class ReorderSectionArgument : ICommandArgument
    {
        public ReorderSectionArgument(ICollection<SectionOrderEntry> orderEntries)
        {
            OrderEntries = orderEntries ?? throw new ArgumentNullException(nameof(orderEntries));
        }

        [JsonProperty("sections")]
        public ICollection<SectionOrderEntry> OrderEntries { get; set; }
    }
}
