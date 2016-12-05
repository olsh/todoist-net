using System;

using Newtonsoft.Json;

using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Models
{
    public class Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Command" /> class.
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="argument">The argument of the command.</param>
        /// <param name="tempId">Temporary resource ID, Optional. Only specified for commands that create new resource (“item_add” command).</param>
        /// <exception cref="System.ArgumentNullException">
        ///     commandType
        ///     or
        ///     argument
        /// </exception>
        public Command(CommandType commandType, ICommandArgument argument, Guid? tempId)
        {
            if (commandType == null)
            {
                throw new ArgumentNullException(nameof(commandType));
            }

            if (argument == null)
            {
                throw new ArgumentNullException(nameof(argument));
            }

            CommandType = commandType;
            Argument = argument;
            TempId = tempId;
            Uid = Guid.NewGuid();
        }

        public Command(CommandType commandType, ICommandArgument argument)
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
