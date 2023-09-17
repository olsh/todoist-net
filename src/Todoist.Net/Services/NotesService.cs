using System.Collections.Generic;
using System.Threading;
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
        public async Task<IEnumerable<Note>> GetAsync(CancellationToken cancellationToken = default)
        {
            var response = await TodoistClient.GetResourcesAsync(cancellationToken, ResourceType.Notes).ConfigureAwait(false);

            return response.Notes;
        }

        /// <inheritdoc/>
        public Task<NoteInfo> GetAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return TodoistClient.PostAsync<NoteInfo>(
                "notes/get",
                new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("note_id", id.ToString()) },
                cancellationToken);
        }
    }
}
