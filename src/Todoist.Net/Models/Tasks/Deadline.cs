using System;
using System.Text.Json.Serialization;

using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a deadline for a task.
    /// </summary>
    public class Deadline
    {
        [JsonConstructor]
        internal Deadline()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Deadline"/> class with the specified date.
        /// </summary>
        /// <param name="date">The date of the deadline.</param>
        public Deadline(DateTime date)
        {
            Date = date;
        }

        /// <summary>
        /// Gets the date of the deadline.
        /// </summary>
        [JsonPropertyName("date")]
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateTime Date { get; internal set; }

        [JsonInclude]
        [JsonPropertyName("lang")]
        internal string Lang { get; } = "en";
    }
}
