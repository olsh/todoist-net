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

        /// <summary>
        /// Deletes a file asynchronous.
        /// </summary>
        /// <param name="fileUrl">The file URL.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task DeleteAsync(string fileUrl)
        {
            var parameters = new List<KeyValuePair<string, string>>
                                 {
                                     new KeyValuePair<string, string>("file_url", fileUrl)
                                 };
            return _todoistClient.PostRawAsync("uploads/delete", parameters);
        }

        /// <summary>
        /// Gets all uploads.
        /// </summary>
        /// <returns>
        /// The uploads.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task<IEnumerable<Upload>> GetAsync()
        {
            return _todoistClient.PostAsync<IEnumerable<Upload>>(
                "uploads/get",
                new List<KeyValuePair<string, string>>());
        }

        /// <summary>
        /// Uploads a file asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileContent">Content of the file.</param>
        /// <returns>The uploaded file.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
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
