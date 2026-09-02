using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal class TemplatesService : ServiceBase, ITemplatesService
    {
        public TemplatesService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<string> ExportAsFileAsync(string projectId, bool useRelativeDates = true, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(projectId, nameof(projectId));

            var query = new Dictionary<string, string>
            {
                { "project_id", projectId },
                { "use_relative_dates", useRelativeDates.ToString().ToLower() }
            };

            return TodoistClient.GetStringAsync("templates/file", query, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<FileBase> ExportAsUrlAsync(string projectId, bool useRelativeDates = true, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(projectId, nameof(projectId));

            var query = new Dictionary<string, string>
            {
                { "project_id", projectId },
                { "use_relative_dates", useRelativeDates.ToString().ToLower() }
            };

            return TodoistClient.GetAsync<FileBase>("templates/url", query, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<TemplateImportResult> ImportIntoProjectAsync(string projectId, string templateId, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(projectId, nameof(projectId));
            ThrowHelper.ThrowIfNullOrEmpty(templateId, nameof(templateId));

            var body = new TemplateImportRequest
            {
                ProjectId = projectId,
                TemplateId = templateId
            };

            return TodoistClient.PostJsonAsync<TemplateImportRequest, TemplateImportResult>(
                "templates/import_into_project_from_template_id", body, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<TemplateImportResult> ImportIntoProjectAsync(string projectId, FileContent fileContent, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(projectId, nameof(projectId));
            ThrowHelper.ThrowIfNull(fileContent, nameof(fileContent));

            var parameters = new Dictionary<string, string>
            {
                { "project_id", projectId }
            };
            var file = new UploadFile(fileContent.ContentStream, "template.csv");

            return TodoistClient.PostFilesAsync<TemplateImportResult>(
                "templates/import_into_project_from_file", new[] { file }, parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<TemplateImportResult> CreateProjectFromFileAsync(
            string name, 
            FileContent fileContent, 
            string workspaceId = null, 
            CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(name, nameof(name));
            ThrowHelper.ThrowIfNull(fileContent, nameof(fileContent));

            var parameters = new Dictionary<string, string>
            {
                { "name", name }
            };
            parameters.AddIfNotNullOrEmpty("workspace_id", workspaceId);

            var file = new UploadFile(fileContent.ContentStream, "template.csv");

            return TodoistClient.PostFilesAsync<TemplateImportResult>(
                "templates/create_project_from_file", new[] { file }, parameters, cancellationToken);
        }
    }
}
