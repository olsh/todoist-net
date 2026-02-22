using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    internal class NotificationCollectionArgument : ICommandArgument
    {
        [JsonPropertyName("ids")]
        public ICollection<string> Ids { get; set; }
    }
}
