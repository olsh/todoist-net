using System;
using System.IO;

using Todoist.Net.Exceptions;
using Todoist.Net.Services;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a file to be uploaded to Todoist.
    /// This class is used internally by the library. Use <see cref="Services.IUploadsService.UploadAsync"/> instead.
    /// </summary>
    public class UploadFile : FileContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadFile"/> class.
        /// For internal use only. Use <see cref="Services.IUploadsService.UploadAsync"/> to upload files.
        /// </summary>
        /// <param name="content">The file content as a stream.</param>
        /// <param name="filename">The filename.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="content"/> or <paramref name="filename"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="content"/> is empty or <paramref name="filename"/> is empty or whitespace.</exception>
        public UploadFile(Stream content, string filename) : base(content)
        {
            ThrowHelper.ThrowIfNull(content, nameof(content));
            ThrowHelper.ThrowIfNullOrWhiteSpace(filename, nameof(filename));

            Filename = filename;

            if (MimeTypeProvider.TryGetMimeType(Filename, out var mimeType))
            {
                MimeType = mimeType;
            }
        }

        /// <inheritdoc cref="UploadFile(Stream, string)"/>
        /// <param name="content">The file content as byte array.</param>
        /// <param name="filename">The filename.</param>
        public UploadFile(byte[] content, string filename) : this(new MemoryStream(content), filename)
        { }

        /// <inheritdoc cref="UploadFile(Stream, string)"/>
        /// <param name="fileStream">The file content as a file stream.</param>
        public UploadFile(FileStream fileStream) : this(fileStream, Path.GetFileName(fileStream.Name))
        { }

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
