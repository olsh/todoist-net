using System;

using Newtonsoft.Json;

using Todoist.Net.Models;

namespace Todoist.Net.Serialization.Converters
{
    internal class ComplexIdConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ComplexId);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var value = reader.Value?.ToString();
            if (!string.IsNullOrEmpty(value))
            {
                return new ComplexId(value);
            }

            return new ComplexId();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var complexId = (ComplexId)value;
            if (complexId.IsEmpty)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(complexId.DynamicId);
            }
        }
    }
}
