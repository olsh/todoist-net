using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("completed_at")]
        public DateTime CompletedAt { get; internal set; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        [JsonPropertyName("content")]
        public string Content { get; internal set; }

        /// <summary>
        /// Gets the task id
        /// </summary>
        /// <value>
        /// The task id
        /// </value>
        [JsonPropertyName("task_id")]
        public long TaskId { get; internal set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonPropertyName("id")]
        public long Id { get; internal set; }

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [JsonPropertyName("labels")]
        public IReadOnlyCollection<long> Labels { get; internal set; }

        /// <summary>
        /// Gets the note count.
        /// </summary>
        /// <value>
        /// The note count.
        /// </value>
        [JsonPropertyName("note_count")]
        public int NoteCount { get; internal set; }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        [JsonPropertyName("notes")]
        public IReadOnlyCollection<Note> Notes { get; internal set; }

        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        [JsonPropertyName("project_id")]
        public long ProjectId { get; internal set; }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [JsonPropertyName("user_id")]
        public long UserId { get; internal set; }

        /// <summary>
        /// Gets the full item object.
        /// </summary>
        /// <remarks>
        /// This property is only available when the <see cref="ItemFilter.AnnotateItems"/> property is set to <c>true</c> 
        /// in the parameter passed to the <see cref="Services.IItemsService.GetCompletedAsync(ItemFilter, System.Threading.CancellationToken)"/> method.
        /// </remarks>
        /// <value>
        /// The full item object.
        /// </value>
        [JsonPropertyName("item_object")]
        public Item ItemObject { get; internal set; }
    }
}
