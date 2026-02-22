using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents complete recurring task argument.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.ICommandArgument" />
    public class CompleteRecurringTaskArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteRecurringTaskArgument" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dueDate">The due date.</param>
        /// <param name="isForward">
        /// Set this argument to <c>true</c> for completion, or <c>false</c> for uncompletion (e.g., via undo). 
        /// By default, this argument is set to <c>true</c> (completion).
        /// </param>
        /// <param name="resetSubtasks">
        /// Set this argument to <c>true</c> to reset subtasks when a recurring task is completed. 
        /// By default, this property is not set (<c>false</c>), and subtasks will retain their existing status when the parent task recurs.
        /// </param>
        public CompleteRecurringTaskArgument(ComplexId id, DueDate dueDate = null, bool? isForward = null, bool? resetSubtasks = null)
        {
            Id = id;
            DueDate = dueDate;
            IsForward = isForward;
            ResetSubtasks = resetSubtasks;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonPropertyName("id")]
        public ComplexId Id { get; }

        /// <summary>
        /// Gets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        [JsonPropertyName("due")]
        public DueDate DueDate { get; }

        /// <summary>
        /// Gets a value indicating whether the tasks should be completed or uncompleted.
         /// </summary>
        /// <value>
        /// If <c>true</c>, means tasks should be completed; if <c>false</c>, means tasks should be uncompleted (e.g., via undo).
        /// </value>
        [JsonPropertyName("is_forward")]
        public bool? IsForward { get; }

        /// <summary>
        /// Gets a value indicating whether the subtasks should be reset or retain their existing status when the parent task recurs.
        /// </summary>
        /// <value>
        /// If <c>true</c>, means subtasks should be reset when a recurring task is completed; 
        /// if <c>false</c>, means subtasks should retain their existing status when the parent task recurs.
        /// </value>
        [JsonPropertyName("reset_subtasks")]
        public bool? ResetSubtasks { get; }
    }
}
