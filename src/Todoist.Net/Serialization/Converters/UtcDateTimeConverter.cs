using Newtonsoft.Json.Converters;

namespace Todoist.Net.Serialization.Converters
{
    internal class UtcDateTimeConverter : IsoDateTimeConverter
    {
        public UtcDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-ddTHH:mm";
        }
    }
}
