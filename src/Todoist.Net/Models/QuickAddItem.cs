using System.Collections.Generic;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a quick add task.
    /// </summary>
    public class QuickAddItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuickAddItem"/> class.
        /// </summary>
        /// <param name="text">The text of the task that is parsed. It can include a due date in free form text, a project name starting with the # character, a label starting with the @ character, and an assignee starting with the + character.</param>
        public QuickAddItem(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Gets or sets the content of the note.
        /// </summary>
        /// <value>
        /// The content of the note.
        /// </value>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the reminder. The date of the reminder, added in free form text.
        /// </summary>
        /// <value>
        /// The reminder.
        /// </value>
        public string Reminder { get; set; }

        /// <summary>
        /// Gets the text of the task that is parsed.
        /// It can include a due date in free form text, a project name starting with the # character, a label starting with the @ character, and an assignee starting with the + character.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        /// <remarks>Example: Task1 @Label1 #Project1 +ExampleUser</remarks>
        public string Text { get; }

        internal ICollection<KeyValuePair<string, string>> ToParameters()
        {
            var parameters = new LinkedList<KeyValuePair<string, string>>();

            parameters.AddLast(new KeyValuePair<string, string>("text", Text));

            if (string.IsNullOrEmpty(Note))
            {
                parameters.AddLast(new KeyValuePair<string, string>("note", Note));
            }

            if (string.IsNullOrEmpty(Text))
            {
                parameters.AddLast(new KeyValuePair<string, string>("reminder", Reminder));
            }

            return parameters;
        }
    }
}
