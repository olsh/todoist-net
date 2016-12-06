using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a base file.
    /// </summary>
    public class FileBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileBase"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public FileBase(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileBase"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileUrl">The file URL.</param>
        public FileBase(string fileName, string fileUrl)
        {
            FileName = fileName;
            FileUrl = fileUrl;
        }

        internal FileBase()
        {
        }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        [JsonProperty("file_name")]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file URL.
        /// </summary>
        /// <value>
        /// The file URL.
        /// </value>
        [JsonProperty("file_url")]
        public string FileUrl { get; set; }
    }
}
