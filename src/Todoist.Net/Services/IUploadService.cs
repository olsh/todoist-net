using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for file attachments management.
    /// </summary>
    public interface IUploadService
    {
        /// <summary>
        /// Deletes a file asynchronous.
        /// </summary>
        /// <param name="fileUrl">The file URL.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(string fileUrl);

        /// <summary>
        /// Gets all uploads.
        /// </summary>
        /// <returns>
        /// The uploads.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Upload>> GetAsync();

        /// <summary>
        /// Uploads a file asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileContent">Content of the file.</param>
        /// <returns>The uploaded file.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<FileAttachment> UploadAsync(string fileName, byte[] fileContent);
    }
}
