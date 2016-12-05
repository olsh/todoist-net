using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Todoist.Net
{
    internal class TodoistRestClient : ITodoistRestClient
    {
        private readonly HttpClient _httpClient;

        public TodoistRestClient()
        {
            // ReSharper disable once ExceptionNotDocumented
            _httpClient = new HttpClient { BaseAddress = new Uri("https://todoist.com/API/v7/") };
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The task.</returns>
        /// <exception cref="ArgumentException">Value cannot be null or empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="parameters"/> is <see langword="null"/></exception>
        public async Task<HttpResponseMessage> GetAsync(
            string resource,
            IEnumerable<KeyValuePair<string, string>> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (string.IsNullOrEmpty(resource))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(resource));
            }

            // ReSharper disable PossibleMultipleEnumeration
            if (parameters.Any())
            {
                using (var content = new FormUrlEncodedContent(parameters))
                {
                    var query = await content.ReadAsStringAsync().ConfigureAwait(false);
                    resource += $"?{query}";
                }
            }

            // ReSharper restore PossibleMultipleEnumeration
            return await _httpClient.GetAsync(resource).ConfigureAwait(false);
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The task.
        /// </returns>
        /// <exception cref="System.ArgumentException">Value cannot be null or empty - resource</exception>
        /// <exception cref="ArgumentNullException"><paramref name="parameters" /> is <see langword="null" /></exception>
        public async Task<HttpResponseMessage> PostAsync(string resource, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (string.IsNullOrEmpty(resource))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(resource));
            }

            using (var content = new FormUrlEncodedContent(parameters))
            {
                return await _httpClient.PostAsync(resource, content).ConfigureAwait(false);
            }
        }
    }
}
