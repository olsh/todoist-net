using System.Text.Json.Serialization;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    internal class DeleteSharedLabelArgument : ICommandArgument
    {
        internal DeleteSharedLabelArgument(string name)
        {
            ThrowHelper.ThrowIfNullOrEmpty(name, nameof(name));

            Name = name;
        }

        [JsonPropertyName("name")]
        public string Name { get; }
    }
}
