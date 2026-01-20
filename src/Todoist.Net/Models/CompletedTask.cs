using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a completed task.
    /// </summary>
    public class CompletedTask
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
        /// Gets the task id.
        /// </summary>
        /// <value>
        /// The task id.
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
        /// Gets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        /// <remarks>
        /// The JSON property name remains "notes" for backwards compatibility with Sync API.
        /// </remarks>
        [JsonPropertyName("notes")]
        public IReadOnlyCollection<Comment> Comments { get; internal set; }

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
        /// Gets the full task object.
        /// </summary>
        /// <remarks>
        /// This property is only available when the <see cref="TaskFilter.AnnotateTasks"/> property is set to <c>true</c> 
        /// in the parameter passed to the <see cref="Services.ITasksService.GetCompletedAsync(TaskFilter, System.Threading.CancellationToken)"/> method.
        /// The JSON property name remains "item_object" for backwards compatibility with Sync API.
        /// </remarks>
        /// <value>
        /// The full task object.
        /// </value>
        [JsonPropertyName("item_object")]
        public DetailedTask TaskObject { get; internal set; }
    }
}
