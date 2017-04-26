using System;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist upload.
    /// </summary>
    public class Upload
    {
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        [JsonProperty("filename")]
        public string FileName { get; internal set; }

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        /// <value>The size of the file.</value>
        [JsonProperty("file_size")]
        public long FileSize { get; internal set; }

        /// <summary>
        /// Gets the type of the file.
        /// </summary>
        /// <value>The type of the file.</value>
        [JsonProperty("file_type")]
        public string FileType { get; internal set; }

        /// <summary>
        /// Gets the file URL.
        /// </summary>
        /// <value>The file URL.</value>
        [JsonProperty("file_url")]
        public string FileUrl { get; internal set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")]
        public long Id { get; internal set; }

        /// <summary>
        /// Gets the ip.
        /// </summary>
        /// <value>The ip.</value>
        [JsonProperty("ip")]
        public string Ip { get; internal set; }

        /// <summary>
        /// Gets the item identifier.
        /// </summary>
        /// <value>The item identifier.</value>
        [JsonProperty("item_id")]
        public long ItemId { get; internal set; }

        /// <summary>
        /// Gets the note identifier.
        /// </summary>
        /// <value>The note identifier.</value>
        [JsonProperty("note_id")]
        public long NoteId { get; internal set; }

        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        /// <value>The project identifier.</value>
        [JsonProperty("project_id")]
        public long ProjectId { get; internal set; }

        /// <summary>
        /// Gets the uploaded.
        /// </summary>
        /// <value>The uploaded.</value>
        [JsonProperty("uploaded")]
        public DateTime Uploaded { get; internal set; }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [JsonProperty("user_id")]
        public long UserId { get; internal set; }
    }
}
