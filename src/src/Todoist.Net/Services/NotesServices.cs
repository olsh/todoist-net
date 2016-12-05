using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;
using Todoist.Net.Models.Types;

namespace Todoist.Net.Services
{
    public class NotesServices : ServiceBase
    {
        public NotesServices(ITodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        public NotesServices(ICollection<Command> queue)
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
        /// <param name="ids">The id of the note.</param>
        /// <returns>The task.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task DeleteAsync(ComplexId ids)
        {
            var command = CreateEntityCommand(ids, CommandType.DeleteNote);
            await ExecuteCommandAsync(command).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates the note asynchronous.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns>
        /// The task.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="note" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task UpdateAsync(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }

            var command = new Command(CommandType.UpdateNote, note);
            await ExecuteCommandAsync(command).ConfigureAwait(false);
        }
    }
}
