using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
        public Task MarkAllReadAsync(CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.SetLastReadNotification, null);
            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task MarkLastReadAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.SetLastReadNotification, id);
            return ExecuteCommandAsync(command, cancellationToken);
        }
    }
}
