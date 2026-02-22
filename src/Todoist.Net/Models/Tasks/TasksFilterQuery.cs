using System.Collections.Generic;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a paginated search query for tasks with common pagination parameters and a project filter.
    /// </summary>
    public class TasksFilterQuery : PaginatedSearchQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TasksFilterQuery" /> class.
        /// </summary>
        /// <param name="query">The filter string to apply to the search query.</param>
        /// <param name="lang">The language to use for the search query.</param>
        /// <param name="cursor">The cursor for pagination continuation.</param>
        /// <param name="limit">The maximum number of items to return.</param>
        public TasksFilterQuery(string query, Language lang = null, string cursor = null, int? limit = null)
            : base(query, cursor, limit)
        {
            Lang = lang;
        }

        /// <summary>
        /// Gets or sets the language to use for the search query.
        /// </summary>
        public Language Lang { get; set; }

        internal override Dictionary<string, string> ToParameters()
        {
            var parameters = base.ToParameters();

            parameters.AddIfNotNullOrEmpty("lang", Lang?.ToString());

            return parameters;
        }
    }
}
