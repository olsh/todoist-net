using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for comments management which can be executed in a transaction.
    /// </summary>
    /// <seealso cref="CommandServiceBase" />
    /// <seealso cref="Todoist.Net.Services.ICommentsCommandService" />
    internal class CommentsCommandService : CommandServiceBase, ICommentsCommandService
    {
        internal CommentsCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal CommentsCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <inheritdoc/>
        public async Task<ComplexId> AddToTaskAsync(Comment comment, ComplexId taskId, CancellationToken cancellationToken = default)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            comment.TaskId = taskId;

            var command = CreateAddCommand(CommandType.AddComment, comment);
            await ExecuteCommandAsync(command, cancellationToken).ConfigureAwait(false);

            return comment.Id;
        }

        /// <inheritdoc/>
        public async Task<ComplexId> AddToProjectAsync(Comment comment, ComplexId projectId, CancellationToken cancellationToken = default)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            comment.ProjectId = projectId;

            var command = CreateAddCommand(CommandType.AddComment, comment);
            await ExecuteCommandAsync(command, cancellationToken).ConfigureAwait(false);

            return comment.Id;
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            var command = CreateEntityCommand(CommandType.DeleteComment, id);
            return ExecuteCommandAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Comment comment, CancellationToken cancellationToken = default)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            var command = new Command(CommandType.UpdateComment, comment);
            return ExecuteCommandAsync(command, cancellationToken);
        }
    }
}
