using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist email management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.IEmailService" />
    public class EmailService : IEmailService
    {
        private readonly IAdvancedTodoistClient _todoistClient;

        internal EmailService(IAdvancedTodoistClient todoistClient)
        {
            _todoistClient = todoistClient;
        }

        /// <inheritdoc/>
        public Task DisableAsync(ObjectType objectType, ComplexId objectId, CancellationToken cancellationToken = default)
        {
            var parameters = CreateParameters(objectType, objectId);

            return _todoistClient.PostRawAsync("emails/disable", parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<EmailInfo> GetOrCreateAsync(ObjectType objectType, ComplexId objectId, CancellationToken cancellationToken = default)
        {
            var parameters = CreateParameters(objectType, objectId);

            return _todoistClient.PostAsync<EmailInfo>("emails/get_or_create", parameters, cancellationToken);
        }

        private static List<KeyValuePair<string, string>> CreateParameters(ObjectType objectType, ComplexId objectId)
        {
            if (objectType == null)
            {
                throw new ArgumentNullException(nameof(objectType));
            }

            var parameters =
                new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>(
                            "obj_type",
                            objectType.ToString()),
                        new KeyValuePair<string, string>(
                            "obj_id",
                            objectId.ToString())
                    };
            return parameters;
        }
    }
}
