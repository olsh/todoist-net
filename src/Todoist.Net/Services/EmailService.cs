using System;
using System.Collections.Generic;
using System.Net.Http;
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

        /// <summary>
        /// Disables an email address for an object.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="objectId">The object identifier.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task DisableAsync(ObjectType objectType, ComplexId objectId)
        {
            var parameters = CreateParameters(objectType, objectId);

            return _todoistClient.PostRawAsync("emails/disable", parameters);
        }

        /// <summary>
        /// Creates a new email address for an object, or gets an existing email.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="objectId">The object identifier.</param>
        /// <returns>
        /// The email information.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task<EmailInfo> GetOrCreateAsync(ObjectType objectType, ComplexId objectId)
        {
            var parameters = CreateParameters(objectType, objectId);

            return _todoistClient.PostAsync<EmailInfo>("emails/get_or_create", parameters);
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
