using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The notes.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Note>> GetAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a note by ID.
        /// </summary>
        /// <param name="id">The ID of the note.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The note.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<NoteInfo> GetAsync(ComplexId id, CancellationToken cancellationToken = default);
    }
}
