using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal abstract class CommandServiceBase
    {
        private readonly ICollection<Command> _queue;

        internal CommandServiceBase(IAdvancedTodoistClient todoistClient)
        {
            TodoistClient = todoistClient;
        }

        internal CommandServiceBase(ICollection<Command> queue)
        {
            _queue = queue;
        }

        internal IAdvancedTodoistClient TodoistClient { get; }

        internal Command CreateAddCommand<T>(CommandType commandType, T entity) where T : BaseEntity
        {
            var tempId = Guid.NewGuid();
            entity.Id = tempId;

            return new Command(commandType, entity, tempId);
        }

        /// <summary>
        /// Creates the collection operation.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>The collection operation command.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        internal Command CreateCollectionCommand(IEnumerable<ComplexId> ids, CommandType commandType)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var command = new Command(commandType, new IdsArgument(ids), null);
            return command;
        }

        /// <summary>
        /// Creates the entity command.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>The command.</returns>
        internal Command CreateEntityCommand(ComplexId id, CommandType commandType)
        {
            return new Command(commandType, new BaseEntity(id), null);
        }

        /// <summary>
        /// Executes the command asynchronous.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        internal async Task ExecuteCommandAsync(Command command)
        {
            if (_queue == null)
            {
                await TodoistClient.ExecuteCommandsAsync(command).ConfigureAwait(false);
                return;
            }

            _queue.Add(command);
        }
    }
}
