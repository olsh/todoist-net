using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Todoist.Net
{
    /// <summary>
    /// Represents a REST client.
    /// </summary>
    public interface ITodoistRestClient : IDisposable
    {
        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="parameters" /> is <see langword="null" /></exception>
        Task<HttpResponseMessage> PostAsync(string resource, IEnumerable<KeyValuePair<string, string>> parameters);

        /// <summary>
        /// Posts the form asynchronous.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="files">The files.</param>
        /// <returns>The response.</returns>
        Task<HttpResponseMessage> PostFormAsync(
            string resource,
            IEnumerable<KeyValuePair<string, string>> parameters,
            IEnumerable<ByteArrayContent> files);
    }
}
