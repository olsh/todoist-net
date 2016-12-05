using System;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;
using Todoist.Net.Models.Types;

namespace Todoist.Net.Services
{
    public interface INotesCommandServices
    {
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
        Task<ComplexId> AddToItemAsync(Note note, ComplexId itemId);

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
        Task<ComplexId> AddToProjectAsync(Note note, ComplexId projectId);

        /// <summary>
        /// Deletes the note asynchronous.
        /// </summary>
        /// <param name="ids">The id of the note.</param>
        /// <returns>The task.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(ComplexId ids);

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
        Task UpdateAsync(Note note);
    }
}