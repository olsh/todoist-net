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
        public async Task<NotesInfo> GetAsync(CancellationToken cancellationToken = default)
        {
            var response = await TodoistClient.GetResourcesAsync(cancellationToken, ResourceType.Notes).ConfigureAwait(false);

            return new NotesInfo
            {
                ItemNotes = response.Notes,
                ProjectNotes = response.ProjectNotes
            };
        }
    }
}
