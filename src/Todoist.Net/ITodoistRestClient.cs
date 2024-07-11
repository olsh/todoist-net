using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Todoist.Net
{
    /// <summary>
    /// Represents a REST client.
    /// </summary>
    public interface ITodoistRestClient : IDisposable
    {
        /// <summary>
        /// Sends a <c>GET</c> request, and handles response asynchronously.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentException">Value cannot be null or empty - resource</exception>
        /// <exception cref="ArgumentNullException"><paramref name="parameters" /> is <see langword="null" /></exception>
        Task<HttpResponseMessage> GetAsync(string resource, IEnumerable<KeyValuePair<string, string>> parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a <c>POST</c> request, and handles response asynchronously.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentException">Value cannot be null or empty - resource</exception>
        /// <exception cref="ArgumentNullException"><paramref name="parameters" /> is <see langword="null" /></exception>
        Task<HttpResponseMessage> PostAsync(string resource, IEnumerable<KeyValuePair<string, string>> parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a <c>POST</c> request with form data, and handles response asynchronously.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="files">The files.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The response.</returns>
        Task<HttpResponseMessage> PostFormAsync(
            string resource,
            IEnumerable<KeyValuePair<string, string>> parameters,
            IEnumerable<ByteArrayContent> files,
            CancellationToken cancellationToken = default);
    }
}
