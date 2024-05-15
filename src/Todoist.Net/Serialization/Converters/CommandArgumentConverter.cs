using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Todoist.Net.Models;

namespace Todoist.Net.Serialization.Converters
{
    internal class CommandArgumentConverter : JsonConverter<ICommandArgument>
    {
        public override ICommandArgument Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Skip deserialization.
            return null;
        }

        public override void Write(Utf8JsonWriter writer, ICommandArgument value, JsonSerializerOptions options)
        {
            // Use type of the implementing class.
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
