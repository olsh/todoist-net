using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Todoist.Net.Models;

namespace Todoist.Net.Serialization.Converters
{
    internal class StringEnumTypeConverter : JsonConverter<StringEnum>
    {
        public override StringEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (StringEnum.TryParse(reader.GetString(), typeToConvert, out var stringEnum))
            {
                return stringEnum;
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, StringEnum value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}
