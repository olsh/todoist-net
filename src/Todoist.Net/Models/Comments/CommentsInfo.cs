using System.Collections.Generic;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents collections of task and project comments.
    /// </summary>
    public class CommentsInfo
    {
        /// <summary>
        /// Gets the task comments.
        /// </summary>
        /// <value>
        /// The task comments.
        /// </value>
        public IReadOnlyCollection<Comment> TaskComments { get; internal set; }

        /// <summary>
        /// Gets the project comments.
        /// </summary>
        /// <value>
        /// The project comments.
        /// </value>
        public IReadOnlyCollection<Comment> ProjectComments { get; internal set; }
    }
}
