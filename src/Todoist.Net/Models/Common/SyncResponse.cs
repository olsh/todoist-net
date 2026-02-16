using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    internal class SyncResponse
    {
        [JsonPropertyName("sync_status")]
        public Dictionary<Guid, CommandResult> SyncStatus { get; set; }

        [JsonPropertyName("temp_id_mapping")]
        public Dictionary<Guid, string> TempIdMappings { get; set; }

        [JsonPropertyName("sync_token")]
        public string SyncToken { get; set; }
    }
}
