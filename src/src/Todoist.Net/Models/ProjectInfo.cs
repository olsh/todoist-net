using System.Collections.Generic;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    public class ProjectInfo
    {
        [JsonProperty("notes")]
        public ICollection<Note> Notes { get; internal set; }

        [JsonProperty("project")]
        public Project Project { get; internal set; }
    }
}
