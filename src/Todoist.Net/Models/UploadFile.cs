namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a file to be uploaded to Todoist.
    /// This class is used internally by the library. Use <see cref="Services.IUploadService.UploadAsync"/> instead.
    /// </summary>
    public class UploadFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadFile"/> class.
        /// For internal use only. Use <see cref="Services.IUploadService.UploadAsync"/> to upload files.
        /// </summary>
        /// <param name="content">The file content as byte array.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="mimeType">The MIME type of the file. Optional.</param>
        public UploadFile(byte[] content, string filename, string mimeType = null)
        {
            Content = content;
            Filename = filename;
            MimeType = mimeType;
        }

        /// <summary>
        /// Gets the file content as byte array.
        /// </summary>
        public byte[] Content { get; }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        public string Filename { get; }

        /// <summary>
        /// Gets the MIME type of the file. Can be null.
        /// </summary>
        public string MimeType { get; }
    }
}
