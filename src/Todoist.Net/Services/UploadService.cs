using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

#if NETSTANDARD2_0
using Microsoft.AspNetCore.StaticFiles;
#else
using System.Web;
#endif

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

        #if NETSTANDARD2_0
        private static readonly FileExtensionContentTypeProvider MimeProvider = new FileExtensionContentTypeProvider();
        #endif

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
#if NETSTANDARD2_0
            MimeProvider.TryGetContentType(fileName, out var mimeType);
#else
            var mimeType = MimeMapping.GetMimeMapping(fileName);
#endif

            var parameters = new Dictionary<string, string>();
            var file = new FormFile(fileContent, fileName, mimeType);
            var files = new[] { file };

            return _todoistClient.PostFormAsync<FileAttachment>("uploads/add", parameters, files, cancellationToken);
        }
    }
}
