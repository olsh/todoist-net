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
        }
    }
}
