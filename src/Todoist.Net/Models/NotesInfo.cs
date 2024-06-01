using System.Collections.Generic;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents collections of item and project notes.
    /// </summary>
    public class NotesInfo
    {
        /// <summary>
        /// Gets the item notes.
        /// </summary>
        /// <value>
        /// The item notes.
        /// </value>
        public IReadOnlyCollection<Note> ItemNotes { get; internal set; }

        /// <summary>
        /// Gets the project notes.
        /// </summary>
        /// <value>
        /// The project notes.
        /// </value>
        public IReadOnlyCollection<Note> ProjectNotes { get; internal set; }
    }
}
