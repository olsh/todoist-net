using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for labels management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.ILabelsCommandService" />
    public interface ILabelsService : ILabelsCommandService
    {
        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The labels.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Label>> GetAsync(CancellationToken cancellationToken = default);
    }
}
