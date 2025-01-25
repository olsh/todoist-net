using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Todoist.Net.Serialization.Converters
{
    internal sealed class DateOnlyConverter : JsonConverter<DateTime>
    {
        private const string CustomDateFormat = "yyyy-MM-dd";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString(), CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(CustomDateFormat));
        }
    }
}
