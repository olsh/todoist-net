using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
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
        public Task<ComplexId> AddToTaskAsync(Comment comment, ComplexId taskId, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfDefaultOrEmpty(taskId, nameof(taskId));

            comment.TaskId = taskId;
            return ExecuteAddCommandAsync(CommandType.AddComment, comment, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ComplexId> AddToProjectAsync(Comment comment, ComplexId projectId, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfDefaultOrEmpty(projectId, nameof(projectId));

            comment.ProjectId = projectId;
            return ExecuteAddCommandAsync(CommandType.AddComment, comment, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Comment comment, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateComment, comment, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return ExecuteEntityCommandAsync(CommandType.DeleteComment, id, cancellationToken);
        }
    }
}
