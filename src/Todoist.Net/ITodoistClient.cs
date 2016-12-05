using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;
using Todoist.Net.Services;

namespace Todoist.Net
{
    public interface ITodoistClient
    {
        INotesServices Notes { get; }

        IProjectService Projects { get; }

        void Dispose();

        /// <summary>
        ///     Executes the commands asynchronous.
        /// </summary>
        /// <param name="commands">The commands.</param>
        /// <returns>The task.</returns>
        /// <exception cref="System.ArgumentNullException">Value cannot be null - commands.</exception>
        /// <exception cref="System.AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task ExecuteCommandsAsync(params Command[] commands);

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T">Type of the result.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<T> GetAsync<T>(string resource, ICollection<KeyValuePair<string, string>> parameters);

        /// <summary>
        /// Gets the resources asynchronous.
        /// </summary>
        /// <param name="resourceTypes">The resource types.</param>
        /// <returns>
        /// All resources.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="resourceTypes"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException">Value cannot be an empty collection.</exception>
        Task<Resources> GetResourcesAsync(params ResourceType[] resourceTypes);
    }
}
