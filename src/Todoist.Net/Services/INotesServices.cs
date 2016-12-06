using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for notes management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.INotesCommandServices" />
    public interface INotesServices : INotesCommandServices
    {
        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <returns>The notes.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Note>> GetAsync();

        /// <summary>
        /// Gets a note by ID.
        /// </summary>
        /// <param name="id">The ID of the note.</param>
        /// <returns>
        /// The note.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<NoteInfo> GetAsync(ComplexId id);
    }
}
