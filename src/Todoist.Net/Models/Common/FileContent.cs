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


        private bool _disposed = false;

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the object and releases any resources.
        /// Override this method in derived classes to dispose additional resources.
        /// </summary>
        /// <param name="disposing">Indicates whether the method is called from the <see cref="Dispose()"/> method (true) or from the finalizer (false).</param>
        /// <remarks>
        /// When overriding this method, make sure to call the base class's <see cref="Dispose(bool)"/> method to ensure that base class resources are also released.
        /// </remarks>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                ContentStream?.Dispose();
            }
            _disposed = true;
        }
    }
}
