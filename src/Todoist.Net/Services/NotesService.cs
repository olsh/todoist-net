using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for notes management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.NotesCommandService" />
    /// <seealso cref="Todoist.Net.Services.INotesServices" />
    internal class NotesService : NotesCommandService, INotesServices
    {
        internal NotesService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <returns>The notes.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task<IEnumerable<Note>> GetAsync()
        {
            var response = await TodoistClient.GetResourcesAsync(ResourceType.Notes).ConfigureAwait(false);

            return response.Notes;
        }

        /// <summary>
        /// Gets a note by ID.
        /// </summary>
        /// <param name="id">The ID of the note.</param>
        /// <returns>
        /// The note.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task<NoteInfo> GetAsync(ComplexId id)
        {
            return TodoistClient.PostAsync<NoteInfo>(
                "notes/get",
                new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("note_id", id.ToString()) });
        }
    }
}
