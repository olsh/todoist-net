using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Todoist.Net.Serialization.Converters
{
    internal class BoolConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    return reader.GetString() == "1";

                case JsonTokenType.Number:
                    return reader.GetInt32() == 1;

                case JsonTokenType.True:
                    return true;

                case JsonTokenType.False:
                    return false;

                default:
                    throw new NotImplementedException();
            }
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value ? 1 : 0);
        }
    }
}
