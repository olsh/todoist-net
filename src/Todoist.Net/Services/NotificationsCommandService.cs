using System;
using System.Collections.Generic;
using System.Net.Http;
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

        /// <summary>
        /// Marks the last read live notification.
        /// </summary>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task MarkAllReadAsync()
        {
            var command = CreateEntityCommand(CommandType.SetLastReadNotification, 0);
            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Marks the last read live notification.
        /// </summary>
        /// <param name="id">The ID of the last read notification.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task MarkLastReadAsync(ComplexId id)
        {
            var command = CreateEntityCommand(CommandType.SetLastReadNotification, id);
            return ExecuteCommandAsync(command);
        }
    }
}
