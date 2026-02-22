using System;
using System.IO;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the contents of a file.
    /// </summary>
    public class FileContent : IDisposable
    {
        /// <summary>
        /// Gets an empty file content instance.
        /// </summary>
        public static FileContent Empty { get; } = new FileContent(Array.Empty<byte>());

        /// <summary>
        /// Initializes a new instance of the <see cref="FileContent"/> class.
        /// </summary>
        /// <param name="content">The file content as a stream.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="content"/> is null.</exception>
        public FileContent(Stream content)
        {
            ThrowHelper.ThrowIfNull(content, nameof(content));

            ContentStream = content;
        }

        /// <inheritdoc cref="FileContent(Stream)"/>
        /// <param name="content">The file content as byte array.</param>        
        public FileContent(byte[] content) : this(new MemoryStream(content))
        { }

        /// <summary>
        /// Gets the file content as a stream.
        /// </summary>
        public Stream ContentStream { get; }

        /// <inheritdoc/>
        public void Dispose()
        {
            ContentStream?.Dispose();
        }
    }
}
