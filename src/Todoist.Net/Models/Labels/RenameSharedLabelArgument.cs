using System.Text.Json.Serialization;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    internal class RenameSharedLabelArgument : ICommandArgument
    {
        internal RenameSharedLabelArgument(string name, string newName)
        {
            ThrowHelper.ThrowIfNullOrEmpty(name, nameof(name));
            ThrowHelper.ThrowIfNullOrEmpty(newName, nameof(newName));

            NameOld = name;
            NameNew = newName;
        }

        [JsonPropertyName("name_old")]
        public string NameOld { get; }

        [JsonPropertyName("name_new")]
        public string NameNew { get; }
    }
}
