using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a completed task.
    /// </summary>
    public class CompletedItem
    {
        /// <summary>
        /// Gets the completed date.
        /// </summary>
        /// <value>
        /// The completed date.
        /// </value>
        [JsonProperty("completed_date")]
        public DateTime CompletedDate { get; internal set; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        [JsonProperty("content")]
        public string Content { get; internal set; }

        /// <summary>
        /// Gets the task id
        /// </summary>
        /// <value>
        /// The task id
        /// </value>
        [JsonProperty("task_id")]
        public long TaskId { get; internal set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("id")]
        public long Id { get; internal set; }

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [JsonProperty("labels")]
        public IReadOnlyCollection<long> Labels { get; internal set; }

        /// <summary>
        /// Gets the note count.
        /// </summary>
        /// <value>
        /// The note count.
        /// </value>
        [JsonProperty("note_count")]
        public int NoteCount { get; internal set; }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        [JsonProperty("notes")]
        public IReadOnlyCollection<Note> Notes { get; internal set; }

        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        [JsonProperty("project_id")]
        public long ProjectId { get; internal set; }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [JsonProperty("user_id")]
        public long UserId { get; internal set; }
    }
}
