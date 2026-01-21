namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist task update.
    /// </summary>
    public class UpdateTask : BaseTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTask" /> class.
        /// </summary>
        /// <param name="id">The id of the task to update.</param>
        public UpdateTask(ComplexId id)
            : base(id)
        {
        }
    }
}
