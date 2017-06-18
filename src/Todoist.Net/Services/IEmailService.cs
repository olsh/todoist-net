using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for Todoist email management.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Disables an email address for an object.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="objectId">The object identifier.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DisableAsync(ObjectType objectType, ComplexId objectId);

        /// <summary>
        /// Creates a new email address for an object, or gets an existing email.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="objectId">The object identifier.</param>
        /// <returns>
        /// The email information.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<EmailInfo> GetOrCreateAsync(ObjectType objectType, ComplexId objectId);
    }
}
