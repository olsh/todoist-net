using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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
        public Task<string> ExportAsFileAsync(ComplexId projectId, CancellationToken cancellationToken = default)
        {
            var parameters = new List<KeyValuePair<string, string>>
                                 {
                                     new KeyValuePair<string, string>("project_id", projectId.ToString())
                                 };
            return _todoistClient.PostRawAsync("templates/export_as_file", parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<FileBase> ExportAsShareableUrlAsync(ComplexId projectId, CancellationToken cancellationToken = default)
        {
            var parameters = new List<KeyValuePair<string, string>>
                                 {
                                     new KeyValuePair<string, string>("project_id", projectId.ToString())
                                 };
            return _todoistClient.PostAsync<FileBase>("templates/export_as_url", parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task ImportIntoProjectAsync(ComplexId projectId, byte[] fileContent, CancellationToken cancellationToken = default)
        {
            var parameters = new List<KeyValuePair<string, string>>
                                 {
                                     new KeyValuePair<string, string>("project_id", projectId.ToString())
                                 };
            var file = new UploadFile(fileContent, "template.csv", "text/csv");
            var files = new[] { file };

            return _todoistClient.PostFormAsync<dynamic>("templates/import_into_project", parameters, files, cancellationToken);
        }
    }
}
