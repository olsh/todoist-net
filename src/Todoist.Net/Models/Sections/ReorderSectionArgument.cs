using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    internal class ReorderSectionArgument : ICommandArgument
    {
        public ReorderSectionArgument(ICollection<SectionOrderEntry> orderEntries)
        {
            OrderEntries = orderEntries ?? throw new ArgumentNullException(nameof(orderEntries));
        }

        [JsonPropertyName("sections")]
        public ICollection<SectionOrderEntry> OrderEntries { get; set; }
    }
}
