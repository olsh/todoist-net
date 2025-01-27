using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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
            return _todoistClient.PostRawAsync("uploads/delete", parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<Upload>> GetAsync(CancellationToken cancellationToken = default)
        {
            return _todoistClient.GetAsync<IEnumerable<Upload>>(
                "uploads/get",
                new List<KeyValuePair<string, string>>(),
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<FileAttachment> UploadAsync(
            string fileName, byte[] fileContent, CancellationToken cancellationToken = default
        )
        {
            var data = new MultipartFormDataContent
            {
                {
                    new ByteArrayContent(fileContent), "file", fileName
                }
            };

            return _todoistClient.PostFormAsync<FileAttachment>("uploads/add", data, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<FileAttachment> UploadAsync(
            string fileName, string mimeType, byte[] fileContent, CancellationToken cancellationToken = default
        )
        {
            var mime = mimeType != null ? MediaTypeHeaderValue.Parse(mimeType) : null;
            var data = new MultipartFormDataContent
            {
                {
                    new ByteArrayContent(fileContent)
                    {
                        Headers = { ContentType = mime }
                    },
                    "file", fileName
                }
            };

            return _todoistClient.PostFormAsync<FileAttachment>("uploads/add", data, cancellationToken);
        }
    }
}
