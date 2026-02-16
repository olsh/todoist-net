using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for file attachments management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.IUploadService" />
    internal class UploadService : IUploadService
    {
        private readonly IAdvancedTodoistClient _todoistClient;

        internal UploadService(IAdvancedTodoistClient todoistClient)
        {
            _todoistClient = todoistClient;
        }

        /// <inheritdoc/>
        public Task DeleteAsync(string fileUrl, CancellationToken cancellationToken = default)
        {
            var parameters = new List<KeyValuePair<string, string>>
                                 {
                                     new KeyValuePair<string, string>("file_url", fileUrl)
                                 };
            return _todoistClient.DeleteRawAsync("uploads", parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<FileAttachment> UploadAsync(
            string fileName, byte[] fileContent, CancellationToken cancellationToken = default
        )
        {
            MimeTypeProvider.TryGetMimeType(fileName, out var mimeType);

            var parameters = new Dictionary<string, string>();
            var file = new UploadFile(fileContent, fileName, mimeType);
            var files = new[] { file };

            return _todoistClient.PostFormAsync<FileAttachment>("uploads", parameters, files, cancellationToken);
        }
    }
}
