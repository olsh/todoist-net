using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Todoist.Net.Models;

namespace Todoist.Net.Serialization.Converters
{
    internal sealed class PriorityConverter : JsonConverter<Priority?>
    {
        public override Priority? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.Number)
                return null;
            var num = reader.GetByte();
            return (Priority)(5 - num);
        }

        public override void Write(Utf8JsonWriter writer, Priority? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteNumberValue((uint)(5 - value.Value));
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
