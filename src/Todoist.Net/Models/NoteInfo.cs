using Newtonsoft.Json;

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
        [JsonProperty("note")]
        public Note Note { get; internal set; }
    }
}
