using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for templates management.
    /// </summary>
    /// <remarks>Templates are only available for Todoist Premium users.</remarks>
    public interface ITemplateService
    {
        /// <summary>
        /// Gets a template for the project as a file asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The CSV template is returned.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<string> ExportAsFileAsync(ComplexId projectId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a template for the project as a shareable URL asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The file object of the template.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<FileBase> ExportAsShareableUrlAsync(ComplexId projectId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Imports a template into a project asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="fileContent">Content of the template.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task ImportIntoProjectAsync(ComplexId projectId, byte[] fileContent, CancellationToken cancellationToken = default);
    }
}
