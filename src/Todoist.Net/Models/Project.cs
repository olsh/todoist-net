using Newtonsoft.Json;

using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Models
{
    public class Project : BaseEntity
    {
        public Project(string name)
        {
            Name = name;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("collapsed")]
        public bool? Collapsed { get; set; }

        [JsonProperty("color")]
        public int? Color { get; set; }

        [JsonProperty("indent")]
        public int? Indent { get; set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("is_archived")]
        public bool IsArchived { get; internal set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; internal set; }

        [JsonProperty("item_order")]
        public int? ItemOrder { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shared")]
        public bool Shared { get; internal set; }
    }
}
