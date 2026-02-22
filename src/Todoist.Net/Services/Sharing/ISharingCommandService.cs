using System;
using System.Net.Http;
using System.Threading;
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
        /// Shares a project.
        /// </summary>
        /// <param name="projectId">The project ID to be shared.</param>
        /// <param name="email">The user email with whom to share the project.</param>
        /// <param name="role">The role of the collaborator.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentException">Value cannot be null or empty.</exception>
        Task ShareProjectAsync(ComplexId projectId, string email, ProjectCollaboratorRole role, CancellationToken cancellationToken = default);

        /// <summary>
        /// Accepts an invitation.
        /// </summary>
        /// <param name="invitationId">The invitation id.</param>
        /// <param name="invitationSecret">The secret fetched from the live notification.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentException">Value cannot be null or empty.</exception>
        Task AcceptInvitationAsync(string invitationId, string invitationSecret, CancellationToken cancellationToken = default);

        /// <summary>
        /// Rejects an invitation.
        /// </summary>
        /// <param name="invitationId">The invitation id.</param>
        /// <param name="invitationSecret">The secret fetched from the live notification.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentException">Value cannot be null or empty.</exception>
        Task RejectInvitationAsync(string invitationId, string invitationSecret, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an invitation.
        /// </summary>
        /// <param name="invitationId">The invitation id.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteInvitationAsync(string invitationId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a person from a shared project.
        /// </summary>
        /// <param name="projectId">The project ID to be affected.</param>
        /// <param name="email">The user email with whom to share the project.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentException">Value cannot be null or empty.</exception>
        Task DeleteCollaboratorAsync(ComplexId projectId, string email, CancellationToken cancellationToken = default);
    }
}
