using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    internal class Command
    {
        [JsonConstructor]
        internal Command(CommandType commandType, ICommandArgument argument, Guid? tempId)
        {
            CommandType = commandType;
            Argument = argument;
            TempId = tempId;
            Uid = Guid.NewGuid();
        }

        internal Command(CommandType commandType, ICommandArgument argument)

            // ReSharper disable once IntroduceOptionalParameters.Global
            : this(commandType, argument, null)
        {
        }

        [JsonPropertyName("args")]
        public ICommandArgument Argument { get; }

        [JsonPropertyName("type")]
        public CommandType CommandType { get; }

        [JsonPropertyName("temp_id")]
        public Guid? TempId { get; }

        [JsonPropertyName("uuid")]
        public Guid Uid { get; }
    }
}
