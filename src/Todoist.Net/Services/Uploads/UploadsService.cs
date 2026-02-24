using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for file attachments management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.IUploadsService" />
    internal class UploadsService : ServiceBase, IUploadsService
    {
        internal UploadsService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<FileAttachment> UploadAsync(UploadFile file, string projectId = null, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNull(file, nameof(file));

            var parameters = new Dictionary<string, string>
            {
                { "file_name", file.Filename }
            };
            parameters.AddIfNotNullOrEmpty("project_id", projectId);

            return TodoistClient.PostFilesAsync<FileAttachment>("uploads", new[] { file }, parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(string fileUrl, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                { "file_url", fileUrl }
            };
            
            return TodoistClient.DeleteAsync("uploads", parameters, cancellationToken);
        }
    }
}
