using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal abstract class CommandServiceBase : ServiceBase
    {
        private readonly ICollection<Command> _queue;

        protected CommandServiceBase(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        protected CommandServiceBase(ICollection<Command> queue)
        {
            _queue = queue;
        }

        protected internal async Task<ComplexId> ExecuteAddCommandAsync<T>(CommandType commandType, T entity, CancellationToken cancellationToken = default) 
            where T : BaseEntity
        {
            ThrowHelper.ThrowIfNull(entity, nameof(entity));
            
            var tempId = entity.Id.TempId;
            if (tempId == Guid.Empty)
            {
                tempId = Guid.NewGuid();
                entity.Id = tempId;
            }

            var command = new Command(commandType, entity, tempId);
            await ExecuteCommandAsync(command, cancellationToken).ConfigureAwait(false);

            // Outside a transaction the command has already been sent, and the temporary ID has been
            // replaced by the persistent ID assigned by the API. Inside a transaction the entity still
            // holds its temporary ID, which is resolved when the transaction is committed.
            return entity.Id;
        }

        protected internal Task ExecuteEntityCommandAsync(CommandType commandType, ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = new Command(commandType, new BaseEntity(id));
            return ExecuteCommandAsync(command, cancellationToken);
        }

        protected internal Task ExecuteCommandAsync(CommandType commandType, CancellationToken cancellationToken = default)
        {
            var command = new Command(commandType, EmptyCommand.Instance);
            return ExecuteCommandAsync(command, cancellationToken);
        }

        protected internal Task ExecuteCommandAsync(CommandType commandType, ICommandArgument argument, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNull(argument, nameof(argument));
            
            var command = new Command(commandType, argument);
            return ExecuteCommandAsync(command, cancellationToken);
        }


        private Task ExecuteCommandAsync(Command command, CancellationToken cancellationToken = default)
        {
            if (_queue == null)
            {
                return TodoistClient.SyncCommandsAsync(new[] { command }, throwOnError: true, cancellationToken: cancellationToken);
            }

            _queue.Add(command);
            return Task.CompletedTask;
        }
    }
}
