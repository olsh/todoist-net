
namespace Todoist.Net.Models
{
    /// <summary>
    /// Class Reminder.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.BaseEntity" />
    public class AddReminder : Reminder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddReminder" /> class.
        /// </summary>
        /// <param name="taskId">The ID of the task.</param>
        /// <param name="type">The type of the reminder.</param>
        public AddReminder(ComplexId taskId, ReminderType type)
        {
            TaskId = taskId;
            Type = type;
        }
    }
}
