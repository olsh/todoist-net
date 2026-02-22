using System.Collections.Generic;

using Todoist.Net.Exceptions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a paginated search query with common pagination parameters and a filter string for search criteria.
    /// </summary>
    public class PaginatedSearchQuery : PaginationQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedSearchQuery" /> class.
        /// </summary>
        /// <param name="query">The filter string to apply to the search query.</param>
        /// <param name="cursor">The cursor for pagination continuation.</param>
        /// <param name="limit">The maximum number of items to return.</param>
        public PaginatedSearchQuery(string query, string cursor = null, int? limit = null)
            : base(cursor, limit)
        {
            ThrowHelper.ThrowIfNullOrEmpty(query, nameof(query));

            Query = query;
        }

        /// <summary>
        /// Gets or sets the filter string to apply to the search query.
        /// </summary>
        public string Query { get; set; }

        internal override Dictionary<string, string> ToParameters()
        {
            var parameters = base.ToParameters();

            parameters.AddIfNotNullOrEmpty("query", Query);

            return parameters;
        }
    }
}
