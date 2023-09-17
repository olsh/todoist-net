using System;
using System.Collections.Generic;
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

        /// <inheritdoc/>
        public Task AcceptInvitationAsync(long id, string invitationSecret)
        {
            if (string.IsNullOrEmpty(invitationSecret))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(invitationSecret));
            }

            var command = new Command(CommandType.AcceptInvitation, new Invitation(id, invitationSecret));
            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task DeleteCollaboratorAsync(ComplexId id, string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(email));
            }

            var command = new Command(CommandType.DeleteCollaborator, new ShareProjectArgument(id, email));
            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task DeleteInvitationAsync(long id)
        {
            var command = new Command(CommandType.DeleteInvitation, new BaseInvitation(id));
            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task RejectInvitationAsync(long id, string invitationSecret)
        {
            if (string.IsNullOrEmpty(invitationSecret))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(invitationSecret));
            }

            var command = new Command(CommandType.RejectInvitation, new Invitation(id, invitationSecret));
            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
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
