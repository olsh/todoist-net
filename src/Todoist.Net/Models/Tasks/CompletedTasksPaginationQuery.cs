using System;
using System.Collections.Generic;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a paginated query for completed tasks with common pagination parameters and a project filter.
    /// </summary>
    public class CompletedTasksPaginationQuery : PaginationQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompletedTasksPaginationQuery" /> class.
        /// </summary>
        /// <remarks>
        /// Used to retrieve a list of completed tasks strictly limited by the specified completion date range (up to 3 months).
        /// </remarks>
        /// <param name="since">The date and time to filter completed tasks from.</param>
        /// <param name="until">The date and time to filter completed tasks until.</param>
        /// <param name="cursor">The cursor for pagination continuation.</param>
        /// <param name="limit">The maximum number of items to return.</param>
        public CompletedTasksPaginationQuery(DateTime since, DateTime until, string cursor = null, int? limit = null)
            : base(cursor, limit)
        {
            Since = since;
            Until = until;
        }

        /// <summary>
        /// Gets or sets the date and time to filter completed tasks from.
        /// </summary>
        public DateTime Since { get; set; }

        /// <summary>
        /// Gets or sets the date and time to filter completed tasks until.
        /// </summary>
        public DateTime Until { get; set; }

        /// <summary>
        /// Gets or sets the ID of the workspace to filter tasks by.
        /// </summary>
        public string WorkspaceId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the project to filter tasks by.
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the section to filter tasks by.
        /// </summary>
        public string SectionId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the parent task to filter tasks by.
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Gets or sets the filter query to filter tasks by.
        /// </summary>
        public string FilterQuery { get; set; }

        /// <summary>
        /// Gets or sets the filter language to filter tasks by.
        /// </summary>
        public string FilterLang { get; set; }

        internal override Dictionary<string, string> ToParameters()
        {
            var parameters = new Dictionary<string, string>(base.ToParameters())
            {
                { "since", Since.ToString("o") },
                { "until", Until.ToString("o") },
                { "workspace_id", WorkspaceId },
                { "project_id", ProjectId },
                { "section_id", SectionId },
                { "parent_id", ParentId },
                { "filter", FilterQuery },
                { "filter_lang", FilterLang }
            };

            return parameters.ToNonEmptyValuesDictionary();
        }
    }
}
