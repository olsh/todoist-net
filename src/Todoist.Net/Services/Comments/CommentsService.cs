using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for comments management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.CommentsCommandService" />
    /// <seealso cref="Todoist.Net.Services.ICommentsService" />
    internal class CommentsService : CommentsCommandService, ICommentsService
    {
        internal CommentsService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<CommentsSyncResponse> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return TodoistClient.SyncResourcesAsync<CommentsSyncResponse>(new[] { ResourceType.Comments }, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<Comment>> GetAsync(CommentsPaginationQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedResponse<Comment>>("comments", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Comment> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(id, nameof(id));

            return TodoistClient.GetAsync<Comment>($"comments/{id}", cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Comment> AddToProjectAndReturnAsync(Comment comment, string projectId, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNull(comment, nameof(comment));
            ThrowHelper.ThrowIfNullOrEmpty(projectId, nameof(projectId));

            comment.ProjectId = projectId;
            return TodoistClient.PostJsonAsync<Comment, Comment>("comments", comment, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Comment> AddToTaskAndReturnAsync(Comment comment, string taskId, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNull(comment, nameof(comment));
            ThrowHelper.ThrowIfNullOrEmpty(taskId, nameof(taskId));

            comment.TaskId = taskId;
            return TodoistClient.PostJsonAsync<Comment, Comment>("comments", comment, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Comment> UpdateAndReturnAsync(string id, Comment comment, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(id, nameof(id));
            ThrowHelper.ThrowIfNull(comment, nameof(comment));

            return TodoistClient.PostJsonAsync<Comment, Comment>($"comments/{id}", comment, cancellationToken);
        }
    }
}
