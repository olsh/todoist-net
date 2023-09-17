using System.Collections.Generic;
using System.Net.Http;
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
        public Task DeleteAsync(string fileUrl)
        {
            var parameters = new List<KeyValuePair<string, string>>
                                 {
                                     new KeyValuePair<string, string>("file_url", fileUrl)
                                 };
            return _todoistClient.PostRawAsync("uploads/delete", parameters);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<Upload>> GetAsync()
        {
            return _todoistClient.PostAsync<IEnumerable<Upload>>(
                "uploads/get",
                new List<KeyValuePair<string, string>>());
        }

        /// <inheritdoc/>
        public Task<FileAttachment> UploadAsync(string fileName, byte[] fileContent)
        {
            var parameters = new List<KeyValuePair<string, string>>
                                 {
                                     new KeyValuePair<string, string>("file_name", fileName)
                                 };
            var files = new[] { new ByteArrayContent(fileContent) };

            return _todoistClient.PostFormAsync<FileAttachment>("uploads/add", parameters, files);
        }
    }
}
