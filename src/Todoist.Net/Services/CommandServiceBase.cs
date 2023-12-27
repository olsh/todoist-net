using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal abstract class CommandServiceBase
    {
        private readonly ICollection<Command> _queue;

        protected CommandServiceBase(IAdvancedTodoistClient todoistClient)
        {
            TodoistClient = todoistClient;
        }

        protected CommandServiceBase(ICollection<Command> queue)
        {
            _queue = queue;
        }

        internal IAdvancedTodoistClient TodoistClient { get; }

        internal Command CreateAddCommand<T>(CommandType commandType, T entity) where T : BaseEntity
        {
            var tempId = entity.Id.TempId;
            if (tempId == Guid.Empty)
            {
                tempId = Guid.NewGuid();
            }
            entity.Id = tempId;

            return new Command(commandType, entity, tempId);
        }

        internal Command CreateEntityCommand(CommandType commandType, ComplexId id)
        {
            return new Command(commandType, new BaseEntity(id));
        }

        /// <summary>
        /// Executes the command asynchronous.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        internal async Task ExecuteCommandAsync(Command command, CancellationToken cancellationToken = default)
        {
            if (_queue == null)
            {
                await TodoistClient.ExecuteCommandsAsync(cancellationToken, command).ConfigureAwait(false);
                return;
            }

            _queue.Add(command);
        }
    }
}
