using System;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for projects management which can be executed in a transaction.
    /// </summary>
    public interface ISharingCommandService
    {
        /// <summary>
        /// Accepts an invitation.
        /// </summary>
        /// <param name="id">The invitation id.</param>
        /// <param name="invitationSecret">The secret fetched from the live notification.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentException">Value cannot be null or empty.</exception>
        Task AcceptInvitationAsync(long id, string invitationSecret);

        /// <summary>
        /// Deletes a person from a shared project.
        /// </summary>
        /// <param name="id">The project ID to be affected.</param>
        /// <param name="email">The user email with whom to share the project.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentException">Value cannot be null or empty.</exception>
        Task DeleteCollaboratorAsync(ComplexId id, string email);

        /// <summary>
        /// Deletes an invitation.
        /// </summary>
        /// <param name="id">The invitation id.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteInvitationAsync(long id);

        /// <summary>
        /// Rejects an invitation.
        /// </summary>
        /// <param name="id">The invitation id.</param>
        /// <param name="invitationSecret">The secret fetched from the live notification.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentException">Value cannot be null or empty.</exception>
        Task RejectInvitationAsync(long id, string invitationSecret);

        /// <summary>
        /// Shares a project.
        /// </summary>
        /// <param name="id">The project ID to be shared.</param>
        /// <param name="email">The user email with whom to share the project.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentException">Value cannot be null or empty.</exception>
        Task ShareProjectAsync(ComplexId id, string email);
    }
}
