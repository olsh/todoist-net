using System.Collections.Generic;
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

        /// <inheritdoc/>
        public async Task<IEnumerable<Note>> GetAsync()
        {
            var response = await TodoistClient.GetResourcesAsync(ResourceType.Notes).ConfigureAwait(false);

            return response.Notes;
        }

        /// <inheritdoc/>
        public Task<NoteInfo> GetAsync(ComplexId id)
        {
            return TodoistClient.PostAsync<NoteInfo>(
                "notes/get",
                new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("note_id", id.ToString()) });
        }
    }
}
