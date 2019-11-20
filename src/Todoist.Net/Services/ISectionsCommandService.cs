using System;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for sections management which can be executed in a transaction.
    /// </summary>
    public interface ISectionsCommandService
    {
        /// <summary>
        /// Add a new section to a project.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <returns>The ID of the section.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="section" /> is <see langword="null" /></exception>
        Task<ComplexId> AddAsync(Section section);

        /// <summary>
        /// Archive a section and all its descendants tasks.
        /// </summary>
        /// <param name="id">The section ID.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task ArchiveAsync(ComplexId id);

        /// <summary>
        /// Delete a section and all its descendants items.
        /// </summary>
        /// <param name="id">The section ID.</param>
        /// <returns> Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(ComplexId id);

        /// <summary>
        /// Unarchive a section.
        /// </summary>
        /// <param name="id">The section ID.</param>
        /// <returns> Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task UnarchiveAsync(ComplexId id);

        /// <summary>
        /// Updates the section.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="section" /> is <see langword="null" /></exception>
        Task UpdateAsync(Section section);
    }
}
