using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
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
        public Task ShareProjectAsync(ComplexId projectId, string email, ProjectCollaboratorRole role, CancellationToken cancellationToken = default)
        {
            var argument = new SharingCollaboratorArgument(projectId, email, role);
            return ExecuteCommandAsync(CommandType.ShareProject, argument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task AcceptInvitationAsync(string invitationId, string invitationSecret, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(invitationId, nameof(invitationId));
            ThrowHelper.ThrowIfNullOrEmpty(invitationSecret, nameof(invitationSecret));

            var invitation = new Invitation(invitationId, invitationSecret);
            return ExecuteCommandAsync(CommandType.AcceptInvitation, invitation, cancellationToken);
        }

        /// <inheritdoc/>
        public Task RejectInvitationAsync(string invitationId, string invitationSecret, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(invitationId, nameof(invitationId));
            ThrowHelper.ThrowIfNullOrEmpty(invitationSecret, nameof(invitationSecret));

            var invitation = new Invitation(invitationId, invitationSecret);
            return ExecuteCommandAsync(CommandType.RejectInvitation, invitation, cancellationToken);
        }
        /// <inheritdoc/>
        public Task DeleteInvitationAsync(string invitationId, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(invitationId, nameof(invitationId));

            var invitation = new Invitation(invitationId);
            return ExecuteCommandAsync(CommandType.DeleteInvitation, invitation, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteCollaboratorAsync(ComplexId projectId, string email, CancellationToken cancellationToken = default)
        {
            var argument = new SharingCollaboratorArgument(projectId, email);
            return ExecuteCommandAsync(CommandType.DeleteCollaborator, argument, cancellationToken);
        }
    }
}
