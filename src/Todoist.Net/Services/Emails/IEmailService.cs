using System.Net.Http;
using System.Threading;
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
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DisableAsync(ObjectType objectType, ComplexId objectId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new email address for an object, or gets an existing email.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="objectId">The object identifier.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The email information.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<EmailInfo> GetOrCreateAsync(ObjectType objectType, ComplexId objectId, CancellationToken cancellationToken = default);
    }
}
