using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist email management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.IEmailsService" />
    internal class EmailService : ServiceBase, IEmailsService
    {
        internal EmailService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<TodoistObjectEmail> GetOrCreateAsync(EmailObjectType objectType, string objectId, CancellationToken cancellationToken = default)
        {
            var body = new ObjectEmailRequest
            {
                ObjectType = objectType,
                ObjectId = objectId
            };

            return TodoistClient.PutJsonAsync<ObjectEmailRequest, TodoistObjectEmail>("emails", body, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DisableAsync(EmailObjectType objectType, string objectId, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                { "obj_type", objectType.ToString() },
                { "obj_id", objectId }
            };

            return TodoistClient.DeleteAsync<ObjectEmailRequest>("emails", parameters, cancellationToken);
        }
    }
}
