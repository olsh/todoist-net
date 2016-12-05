using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Todoist.Net
{
    public interface ITodoistRestClient : IDisposable
    {
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The task.</returns>
        /// <exception cref="ArgumentException">Value cannot be null or empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="parameters"/> is <see langword="null"/></exception>
        Task<HttpResponseMessage> GetAsync(string resource, IEnumerable<KeyValuePair<string, string>> parameters);

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The task.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="parameters" /> is <see langword="null" /></exception>
        Task<HttpResponseMessage> PostAsync(string resource, IEnumerable<KeyValuePair<string, string>> parameters);
    }
}
