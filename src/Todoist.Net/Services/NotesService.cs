using System.Threading;
using System.Threading.Tasks;

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
        public async Task<CommentsInfo> GetAsync(CancellationToken cancellationToken = default)
        {
            var response = await TodoistClient.GetResourcesAsync(cancellationToken, ResourceType.Comments).ConfigureAwait(false);

            return new CommentsInfo
            {
                TaskComments = response.Comments,
                ProjectComments = response.ProjectComments
            };
        }
    }
}
