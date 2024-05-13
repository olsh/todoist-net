using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Todoist.Net.Models;

namespace Todoist.Net.Serialization.Converters
{
    internal class ComplexIdConverter : JsonConverter<ComplexId>
    {
        public override ComplexId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (!string.IsNullOrEmpty(value))
            {
                return new ComplexId(value);
            }

            return new ComplexId();
        }

        public override void Write(Utf8JsonWriter writer, ComplexId value, JsonSerializerOptions options)
        {
            if (value.IsEmpty)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteStringValue(value.DynamicId.ToString());
            }
        }
    }
}
