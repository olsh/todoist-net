using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents an information about a Todoist note.
    /// </summary>
    public class NoteInfo
    {
        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <value>The note.</value>
        [JsonPropertyName("note")]
        public Note Note { get; internal set; }
    }
}
