using System.Collections.Generic;
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
        public Task MarkAllReadAsync()
        {
            var command = CreateEntityCommand(CommandType.SetLastReadNotification, null);
            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task MarkLastReadAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.SetLastReadNotification, id);
            return ExecuteCommandAsync(command);
        }
    }
}
