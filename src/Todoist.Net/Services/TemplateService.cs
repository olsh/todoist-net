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

        /// <inheritdoc/>
        public Task<string> ExportAsFileAsync(ComplexId projectId)
        {
            var parameters = new List<KeyValuePair<string, string>>
                                 {
                                     new KeyValuePair<string, string>("project_id", projectId.ToString())
                                 };
            return _todoistClient.PostRawAsync("templates/export_as_file", parameters);
        }

        /// <inheritdoc/>
        public Task<FileBase> ExportAsShareableUrlAsync(ComplexId projectId)
        {
            var parameters = new List<KeyValuePair<string, string>>
                                 {
                                     new KeyValuePair<string, string>("project_id", projectId.ToString())
                                 };
            return _todoistClient.PostAsync<FileBase>("templates/export_as_url", parameters);
        }

        /// <inheritdoc/>
        public Task ImportIntoProjectAsync(ComplexId projectId, byte[] fileContent)
        {
            var parameters = new List<KeyValuePair<string, string>>
                                 {
                                     new KeyValuePair<string, string>("project_id", projectId.ToString())
                                 };
            var files = new[] { new ByteArrayContent(fileContent) };

            return _todoistClient.PostFormAsync<dynamic>("templates/import_into_project", parameters, files);
        }
    }
}
