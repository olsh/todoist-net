using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Todoist.Net.Models;

namespace Todoist.Net.Serialization.Converters
{
    internal sealed class CommandResultConverter : JsonConverter<CommandResult>
    {
        public override CommandResult Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                if (!string.Equals(stringValue, CommandResult.SuccessValue, StringComparison.OrdinalIgnoreCase))
                {
                    throw new JsonException($"Unable to parse the value '{stringValue}' to a {nameof(CommandResult)} object.");
                }
                return CommandResult.Success;
            }
            var error = JsonSerializer.Deserialize<CommandError>(ref reader, options);

            return CommandResult.Fail(error);
        }

        public override void Write(Utf8JsonWriter writer, CommandResult value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
