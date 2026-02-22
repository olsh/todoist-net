using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal class NotificationsCommandService : CommandServiceBase, INotificationsCommandService
    {
        internal NotificationsCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal NotificationsCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <inheritdoc/>
        public Task SetLastKnownAsync(string id, CancellationToken cancellationToken = default)
        {
            return ExecuteEntityCommandAsync(CommandType.SetLastReadNotification, id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task MarkAllReadAsync(CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.MarkAllReadNotification, cancellationToken);
        }

        /// <inheritdoc/>
        public Task MarkReadAsync(ICollection<string> ids, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(ids, nameof(ids));

            var argument = new NotificationCollectionArgument { Ids = ids };

            return ExecuteCommandAsync(CommandType.MarkReadNotification, argument, cancellationToken);
        }

        /// <inheritdoc/>
        public Task MarkUnreadAsync(ICollection<string> ids, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(ids, nameof(ids));

            var argument = new NotificationCollectionArgument { Ids = ids };

            return ExecuteCommandAsync(CommandType.MarkUnreadNotification, argument, cancellationToken);
        }
    }
}
