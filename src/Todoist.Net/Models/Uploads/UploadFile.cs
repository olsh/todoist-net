using System;

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
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="content"/> or <paramref name="filename"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="content"/> is empty or <paramref name="filename"/> is empty or whitespace.</exception>
        public UploadFile(byte[] content, string filename, string mimeType = null)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (filename == null)
            {
                throw new ArgumentNullException(nameof(filename));
            }

            if (content.Length == 0)
            {
                throw new ArgumentException("File content cannot be empty.", nameof(content));
            }

            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentException("Filename cannot be empty or whitespace.", nameof(filename));
            }

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
