using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a quick add task.
    /// </summary>
    public class QuickAddTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuickAddTask"/> class.
        /// </summary>
        /// <param name="text">
        /// The text of the task that is parsed. 
        /// It can include a due date in free form text, a project name starting with the # character, a label starting with the @ character, and an assignee starting with the + character.
        /// </param>
        public QuickAddTask(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Gets or sets the content of the comment.
        /// </summary>
        /// <value>
        /// The content of the comment.
        /// </value>
        /// <remarks>
        /// In quick-add requests, this value is sent as the note field in the API payload.
        /// </remarks>
        [JsonPropertyName("note")]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the reminder. The date of the reminder, added in free form text.
        /// </summary>
        /// <value>
        /// The reminder.
        /// </value>
        [JsonPropertyName("reminder")]
        public string Reminder { get; set; }

        /// <summary>
        /// Gets the text of the task that is parsed.
        /// It can include a due date in free form text, a project name starting with the # character, a label starting with the @ character, and an assignee starting with the + character.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        /// <remarks>Example: Task1 @Label1 #Project1 +ExampleUser</remarks>
        [JsonPropertyName("text")]
        public string Text { get; }

        /// <summary>
        /// Gets or sets a value indicating whether automatic reminder should be enabled.
        /// </summary>
        /// <value>The automatic reminder flag.</value>
        [JsonPropertyName("auto_reminder")]
        public bool? AutoReminder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether quick-add metadata should be returned.
        /// </summary>
        /// <value>The metadata flag.</value>
        [JsonPropertyName("meta")]
        public bool? Meta { get; set; }
    }
}
