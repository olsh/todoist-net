using System;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a state of a recurring Todoist task.
    /// </summary>
    public class RecurringItemState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecurringItemState"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public RecurringItemState(ComplexId id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the date string.
        /// </summary>
        /// <value>The date string.</value>
        /// <remarks>The date of the task, added in free form text, for example it can be every day @ 10.</remarks>
        public string DateString { get; set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public ComplexId Id { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the task is to be completed.
        /// </summary>
        /// <value><c>true</c> if this instance is forward; otherwise, <c>false</c>.</value>
        public bool? IsForward { get; set; }

        /// <summary>
        /// Gets or sets the new date.
        /// </summary>
        /// <value>The new date.</value>
        public DateTime? NewDate { get; set; }
    }
}
