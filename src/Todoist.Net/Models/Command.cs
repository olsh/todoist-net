using System;

using Newtonsoft.Json;

using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Models
{
    internal class Command
    {
        internal Command(CommandType commandType, ICommandArgument argument, Guid? tempId)
        {
            CommandType = commandType;
            Argument = argument;
            TempId = tempId;
            Uid = Guid.NewGuid();
        }

        internal Command(CommandType commandType, ICommandArgument argument)

            // ReSharper disable once IntroduceOptionalParameters.Global
            : this(commandType, argument, Guid.NewGuid())
        {
        }

        [JsonProperty("args")]
        public ICommandArgument Argument { get; }

        [JsonConverter(typeof(CommandTypeConverter))]
        [JsonProperty("type")]
        public CommandType CommandType { get; }

        [JsonProperty("temp_id")]
        public Guid? TempId { get; }

        [JsonProperty("uuid")]
        public Guid Uid { get; }
    }
}
