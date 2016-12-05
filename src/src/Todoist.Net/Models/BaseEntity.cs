using Newtonsoft.Json;

using Todoist.Net.Models.Types;

namespace Todoist.Net.Models
{
    public class BaseEntity : ICommandArgument
    {
        public BaseEntity()
        {
        }

        public BaseEntity(ComplexId id)
        {
            Id = id;
        }

        [JsonProperty("id")]
        public ComplexId Id { get; set; }

        public bool ShouldSerializeId()
        {
            return !Id.IsEmpty;
        }
    }
}
