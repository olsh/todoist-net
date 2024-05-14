using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Todoist.Net.Models;

namespace Todoist.Net.Serialization.Converters
{
    internal class ComplexIdConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(ComplexId) || typeToConvert == typeof(ComplexId?);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == typeof(ComplexId))
            {
                return new NonNullableComplexIdConverter();
            }
            if (typeToConvert == typeof(ComplexId?))
            {
                return new NullableComplexIdConverter();
            }
            throw new NotImplementedException();
        }


        private class NonNullableComplexIdConverter : JsonConverter<ComplexId>
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

        private class NullableComplexIdConverter : JsonConverter<ComplexId?>
        {
            public override bool HandleNull => true;

            public override ComplexId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var value = reader.GetString();
                if (value is null)
                {
                    return null;
                }
                if (string.IsNullOrEmpty(value))
                {
                    return new ComplexId();
                }
                return new ComplexId(value);
            }

            public override void Write(Utf8JsonWriter writer, ComplexId? value, JsonSerializerOptions options)
            {
                if (!value.HasValue)
                {
                    writer.WriteNullValue();
                    return;
                }

                var complexId = value.Value;
                if (complexId.IsEmpty)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStringValue(complexId.DynamicId.ToString());
                }
            }
        }

    }
}
