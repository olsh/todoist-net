using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents calendar view settings.
    /// </summary>
    public class CalendarViewSettings
    {
        /// <summary>
        /// Gets or sets the calendar view layout.
        /// </summary>
        [JsonPropertyName("layout")]
        public CalendarViewLayout Layout { get; set; }
    }
}
