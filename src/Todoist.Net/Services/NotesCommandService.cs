using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for notes management which can be executes in a transaction.
    /// </summary>
    /// <seealso cref="CommandServiceBase" />
    /// <seealso cref="Todoist.Net.Services.INotesCommandServices" />
    internal class NotesCommandService : CommandServiceBase, INotesCommandServices
    {
        internal NotesCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal NotesCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <inheritdoc/>
        public async Task<ComplexId> AddToItemAsync(Note note, ComplexId itemId)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }

            note.ItemId = itemId;

            var command = CreateAddCommand(CommandType.AddNote, note);
            await ExecuteCommandAsync(command).ConfigureAwait(false);

            return note.Id;
        }

        /// <inheritdoc/>
        public async Task<ComplexId> AddToProjectAsync(Note note, ComplexId projectId)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }

            note.ProjectId = projectId;

            var command = CreateAddCommand(CommandType.AddNote, note);
            await ExecuteCommandAsync(command).ConfigureAwait(false);

            return note.Id;
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.DeleteNote, id);
            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }

            var command = new Command(CommandType.UpdateNote, note);
            return ExecuteCommandAsync(command);
        }
    }
}
