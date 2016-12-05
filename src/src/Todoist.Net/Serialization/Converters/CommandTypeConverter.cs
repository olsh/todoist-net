using System;

using Newtonsoft.Json;

using Todoist.Net.Models;

namespace Todoist.Net.Serialization.Converters
{
    internal class CommandTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CommandType);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((CommandType)value).Command);
        }
    }
}
