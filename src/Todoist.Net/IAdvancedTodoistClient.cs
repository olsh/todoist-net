using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net
{
    internal interface IAdvancedTodoistClient : ITodoistClient
    {
        /// <summary>
        /// Executes the commands asynchronous.
        /// </summary>
        /// <param name="commands">The commands.</param>
        /// <returns>
        /// Returns <see cref="Task{TResult}" />. The task object representing the asynchronous operation
        /// that at completion returns the commands execution sync_token.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Value cannot be null - commands.</exception>
        /// <exception cref="System.AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentException">Value cannot be an empty collection.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<string> ExecuteCommandsAsync(params Command[] commands);

        /// <summary>
        /// Executes the commands asynchronous.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <param name="commands">The commands.</param>
        /// <returns>
        /// Returns <see cref="Task{TResult}" />. The task object representing the asynchronous operation
        /// that at completion returns the commands execution sync_token.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Value cannot be null - commands.</exception>
        /// <exception cref="System.AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentException">Value cannot be an empty collection.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<string> ExecuteCommandsAsync(CancellationToken cancellationToken, params Command[] commands);

        /// <summary>
        /// Sends a <c>GET</c> request, and handles response asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of the result.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The result.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<T> GetAsync<T>(string resource, ICollection<KeyValuePair<string, string>> parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Posts the request asynchronous.
        /// </summary>
        /// <typeparam name="T">Type of the result.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The result.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<T> PostAsync<T>(string resource, ICollection<KeyValuePair<string, string>> parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Posts the asynchronous and returns a raw content.
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="files">The files.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The result.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<T> PostFormAsync<T>(
            string resource,
            ICollection<KeyValuePair<string, string>> parameters,
            IEnumerable<ByteArrayContent> files,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Posts the asynchronous and returns a raw content.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The result.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<string> PostRawAsync(string resource, ICollection<KeyValuePair<string, string>> parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Processes the request asynchronous.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<T> ProcessPostAsync<T>(string resource, ICollection<KeyValuePair<string, string>> parameters, CancellationToken cancellationToken = default);
    }
}
