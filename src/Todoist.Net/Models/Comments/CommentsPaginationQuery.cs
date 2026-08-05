using System.Collections.Generic;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a paginated query for comments with common pagination parameters and a project filter.
    /// </summary>
    public class CommentsPaginationQuery : PaginationQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsPaginationQuery" /> class.
        /// </summary>
        /// <param name="projectId">The ID of the project to filter comments by.</param>
        /// <param name="taskId">The ID of the task to filter comments by.</param>
        /// <param name="cursor">The cursor for pagination continuation.</param>
        /// <param name="limit">The maximum number of items to return.</param>
        public CommentsPaginationQuery(string projectId = null, string taskId = null, string cursor = null, int? limit = null)
            : base(cursor, limit)
        {
            ProjectId = projectId;
            TaskId = taskId;
        }

        /// <summary>
        /// Gets or sets the ID of the project to filter comments by.
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary> 
        /// Gets or sets the ID of the task to filter comments by.
        /// </summary>
        public string TaskId { get; set; }

        internal override Dictionary<string, string> ToParameters()
        {
            var parameters = new Dictionary<string, string>(base.ToParameters())
            {
                { "project_id", ProjectId },
                { "task_id", TaskId }
            };
            
            return parameters.ToNonEmptyValuesDictionary();
        }
    }
}
