using System;
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
    public interface ITemplatesService
    {
        /// <summary>
        /// Gets a template for the project as a file asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="useRelativeDates">Whether to use relative dates in the template.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The CSV template is returned.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<string> ExportAsFileAsync(string projectId, bool useRelativeDates = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a template for the project as a shareable URL asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="useRelativeDates">Whether to use relative dates in the template.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The file object of the template.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<FileBase> ExportAsUrlAsync(string projectId, bool useRelativeDates = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// Imports a template into a project from a template identifier asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="templateId">The template identifier.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<TemplateImportResult> ImportIntoProjectAsync(string projectId, string templateId, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Imports a template into a project from a template file asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="fileContent">Content of the template.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<TemplateImportResult> ImportIntoProjectAsync(string projectId, FileContent fileContent, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new project from a template file.
        /// </summary>
        /// <param name="name">The new project name.</param>
        /// <param name="fileContent">Content of the template file.</param>
        /// <param name="workspaceId">Optional workspace identifier.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The template import summary.</returns>
        /// <exception cref="ArgumentException"><paramref name="name"/> is null or empty.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="fileContent"/> is null.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<TemplateImportResult> CreateProjectFromFileAsync(string name, FileContent fileContent, string workspaceId = null, CancellationToken cancellationToken = default);
    }
}
