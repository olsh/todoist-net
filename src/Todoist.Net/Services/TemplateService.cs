using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal class TemplateService : ITemplateService
    {
        private readonly IAdvancedTodoistClient _todoistClient;

        public TemplateService(IAdvancedTodoistClient todoistClient)
        {
            _todoistClient = todoistClient;
        }

        /// <summary>
        /// Gets a template for the project as a file asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>The CSV template is returned.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task<string> ExportAsFileAsync(ComplexId projectId)
        {
            var parameters = new List<KeyValuePair<string, string>>
                                 {
                                     new KeyValuePair<string, string>("project_id", projectId.ToString())
                                 };
            return await _todoistClient.PostRawAsync("templates/export_as_file", parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a template for the project as a shareable URL asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>The file object of the template.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task<FileBase> ExportAsShareableUrlAsync(ComplexId projectId)
        {
            var parameters = new List<KeyValuePair<string, string>>
                                 {
                                     new KeyValuePair<string, string>("project_id", projectId.ToString())
                                 };
            return await _todoistClient.PostAsync<FileBase>("templates/export_as_url", parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Imports a template into a project asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="fileContent">Content of the template.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task ImportIntoProjectAsync(ComplexId projectId, byte[] fileContent)
        {
            var parameters = new List<KeyValuePair<string, string>>
                                 {
                                     new KeyValuePair<string, string>("project_id", projectId.ToString())
                                 };
            var files = new[] { new ByteArrayContent(fileContent) };

            await
                _todoistClient.PostFormAsync<dynamic>("templates/import_into_project", parameters, files)
                    .ConfigureAwait(false);
        }
    }
}
