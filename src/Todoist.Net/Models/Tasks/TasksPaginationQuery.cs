using System.Collections.Generic;
using System.Linq;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a paginated query for tasks with common pagination parameters and a project filter.
    /// </summary>
    public class TasksPaginationQuery : PaginationQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TasksPaginationQuery" /> class.
        /// </summary>
        /// <param name="cursor">The cursor for pagination continuation.</param>
        /// <param name="limit">The maximum number of items to return.</param>
        public TasksPaginationQuery(string cursor = null, int? limit = null)
            : base(cursor, limit)
        {
        }

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
        /// Gets or sets the label to filter tasks by.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the list of IDs to filter tasks by.
        /// </summary>
        public ICollection<string> Ids { get; set; }

        internal override Dictionary<string, string> ToParameters()
        {
            var idsValue = Ids != null && Ids.Any() ? $"{string.Join(",", Ids)}" : null;

            var parameters = new Dictionary<string, string>(base.ToParameters())
            {
                { "project_id", ProjectId },
                { "section_id", SectionId },
                { "parent_id", ParentId },
                { "label", Label },
                { "ids", idsValue }
            };

            return parameters.ToNonEmptyValuesDictionary();
        }
    }
}
