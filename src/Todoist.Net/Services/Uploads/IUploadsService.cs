using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for file attachments management.
    /// </summary>
    /// <remarks>
    /// This API v1-aligned surface supports upload and delete operations for attachments; list operations are intentionally not exposed.
    /// </remarks>
    public interface IUploadsService
    {
        /// <summary>
        /// Uploads a file asynchronous.
        /// </summary>
        /// <param name="file">The file to upload.</param>
        /// <param name="projectId">The project ID to associate the file with.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The uploaded file.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<FileAttachment> UploadAsync(UploadFile file, string projectId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a file asynchronous.
        /// </summary>
        /// <param name="fileUrl">The file URL.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(string fileUrl, CancellationToken cancellationToken = default);
    }
}
