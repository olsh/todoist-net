using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for ID mappings between API versions.
    /// </summary>
    public interface IIdsService
    {
        /// <summary>
        /// Gets ID mappings for an object type and list of IDs.
        /// </summary>
        /// <param name="objectName">The object name (for example: projects, tasks, sections).</param>
        /// <param name="objectIds">The object IDs.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The ID mappings.</returns>
        /// <exception cref="ArgumentException"><paramref name="objectName"/> is null/empty or <paramref name="objectIds"/> is empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="objectIds"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IReadOnlyCollection<IdMapping>> GetMappingsAsync(
            MappingObjectName objectName, 
            ICollection<string> objectIds, 
            CancellationToken cancellationToken = default);
    }
}
