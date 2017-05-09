using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for sharing management which can be executed in a transaction.
    /// </summary>
    /// <seealso cref="CommandServiceBase" />
    internal class SharingCommandService : CommandServiceBase, ISharingCommandService
    {
        internal SharingCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal SharingCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <summary>
        /// Accepts an invitation.
        /// </summary>
        /// <param name="id">The invitation id.</param>
        /// <param name="invitationSecret">The secret fetched from the live notification.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentException">Value cannot be null or empty.</exception>
        public Task AcceptInvitationAsync(long id, string invitationSecret)
        {
            if (string.IsNullOrEmpty(invitationSecret))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(invitationSecret));
            }

            var command = new Command(CommandType.AcceptInvitation, new Invitation(id, invitationSecret));
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Deletes a person from a shared project.
        /// </summary>
        /// <param name="id">The project ID to be affected.</param>
        /// <param name="email">The user email with whom to share the project.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentException">Value cannot be null or empty.</exception>
        public Task DeleteCollaboratorAsync(ComplexId id, string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(email));
            }

            var command = new Command(CommandType.DeleteCollaborator, new ShareProjectArgument(id, email));
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Deletes an invitation.
        /// </summary>
        /// <param name="id">The invitation id.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task DeleteInvitationAsync(long id)
        {
            var command = new Command(CommandType.DeleteInvitation, new BaseInvitation(id));
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Rejects an invitation.
        /// </summary>
        /// <param name="id">The invitation id.</param>
        /// <param name="invitationSecret">The secret fetched from the live notification.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentException">Value cannot be null or empty.</exception>
        public Task RejectInvitationAsync(long id, string invitationSecret)
        {
            if (string.IsNullOrEmpty(invitationSecret))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(invitationSecret));
            }

            var command = new Command(CommandType.RejectInvitation, new Invitation(id, invitationSecret));
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Shares a project.
        /// </summary>
        /// <param name="id">The project ID to be shared.</param>
        /// <param name="email">The user email with whom to share the project.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentException">Value cannot be null or empty.</exception>
        public Task ShareProjectAsync(ComplexId id, string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(email));
            }

            var command = new Command(CommandType.ShareProject, new ShareProjectArgument(id, email));
            return ExecuteCommandAsync(command);
        }
    }
}
