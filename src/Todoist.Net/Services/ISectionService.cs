using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for sections management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.ISectionsCommandService" />
    public interface ISectionService : ISectionsCommandService
    {
        /// <summary>
        /// Gets a section by ID.
        /// </summary>
        /// <param name="id">The ID of the section.</param>
        /// <returns>
        /// The section.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<Section> GetAsync(ComplexId id);
    }
}
