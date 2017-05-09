using System;
using System.Collections.Generic;
using System.Net.Http;
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

        /// <summary>
        /// Adds the note asynchronous.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns>
        /// The note ID.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="note" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
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

        /// <summary>
        /// Adds the note asynchronous.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <param name="projectId">The project ID.</param>
        /// <returns>
        /// The note ID.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="note" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
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

        /// <summary>
        /// Deletes the note asynchronous.
        /// </summary>
        /// <param name="id">The ID of the note.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task DeleteAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.DeleteNote, id);
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Updates the note asynchronous.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="note" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
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
