using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Todoist.Net.Models;

namespace Todoist.Net.Serialization.Converters
{
    internal sealed class StringEnumTypeConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsSubclassOf(typeof(StringEnum));
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(typeof(StringEnumTypeConverterInner<>).MakeGenericType(typeToConvert));
        }


        private sealed class StringEnumTypeConverterInner<T> : JsonConverter<T> where T : StringEnum
        {
            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (StringEnum.TryParse(reader.GetString(), out T stringEnum))
                {
                    return stringEnum;
                }

                return null;
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.Value);
            }

            public override T ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var value = reader.GetString();
                if (StringEnum.TryParse(value, out T stringEnum))
                {
                    return stringEnum;
                }

                // Unlike a property value, a dictionary key cannot be null,
                // so an unknown value has to be reported instead of being silently dropped.
                throw new JsonException($"Unknown {typeToConvert.Name} value used as a property name: '{value}'.");
            }

            public override void WriteAsPropertyName(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                writer.WritePropertyName(value.Value);
            }
        }
    }
}
