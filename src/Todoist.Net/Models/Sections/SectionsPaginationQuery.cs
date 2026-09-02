using System.Collections.Generic;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a paginated query for sections with common pagination parameters and a project filter.
    /// </summary>
    public class SectionsPaginationQuery : PaginationQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SectionsPaginationQuery" /> class.
        /// </summary>
        /// <param name="projectId">The ID of the project to filter sections by.</param>
        /// <param name="cursor">The cursor for pagination continuation.</param>
        /// <param name="limit">The maximum number of items to return.</param>
        public SectionsPaginationQuery(string projectId = null, string cursor = null, int? limit = null)
            : base(cursor, limit)
        {
            ProjectId = projectId;
        }

        /// <summary>
        /// Gets or sets the ID of the project to filter sections by.
        /// </summary>
        public string ProjectId { get; set; }

        internal override Dictionary<string, string> ToParameters()
        {
            var parameters = base.ToParameters();

            parameters.AddIfNotNullOrEmpty("project_id", ProjectId);

            return parameters;
        }
    }
}
