using Newtonsoft.Json;

using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Models
{
    public class Label : BaseEntity
    {
        public Label(string name)
        {
            Name = name;
        }

        [JsonProperty("color")]
        public int Color { get; set; }

        [JsonProperty("is_deleted")]
        [JsonConverter(typeof(BoolConverter))]
        public bool IsDeleted { get; internal set; }

        [JsonProperty("item_order")]
        public int ItemOrder { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
