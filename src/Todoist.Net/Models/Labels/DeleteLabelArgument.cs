using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    internal class DeleteLabelArgument : BaseEntity
    {
        internal DeleteLabelArgument(ComplexId id, bool keepAsShared = false)
            : base(id)
        {
            if (keepAsShared)
            {
                Cascade = "none";
            }
        }

        [JsonPropertyName("cascade")]
        public string Cascade { get; }
    }
}
