using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents an attachment.
    /// </summary>
    public class FileAttachment : FileBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileAttachment"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public FileAttachment(string fileName)
            : base(fileName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAttachment"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileUrl">The file URL.</param>
        public FileAttachment(string fileName, string fileUrl)
            : base(fileName, fileUrl)
        {
        }

        internal FileAttachment()
        {
        }

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        /// <value>
        /// The size of the file.
        /// </value>
        [JsonProperty("file_size")]
        public int FileSize { get; internal set; }

        /// <summary>
        /// Gets the type of the file.
        /// </summary>
        /// <value>
        /// The type of the file.
        /// </value>
        [JsonProperty("file_type")]
        public string FileType { get; internal set; }

        /// <summary>
        /// Gets the state of the upload.
        /// </summary>
        /// <value>
        /// The state of the upload.
        /// </value>
        [JsonProperty("upload_state")]
        public string UploadState { get; internal set; }
    }
}
