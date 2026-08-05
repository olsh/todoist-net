using System.Collections.Generic;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents common pagination query parameters for paginated API requests.
    /// </summary>
    public class PaginationQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaginationQuery" /> class.
        /// </summary>
        /// <param name="cursor">The cursor for pagination continuation.</param>
        /// <param name="limit">The maximum number of items to return.</param>
        public PaginationQuery(string cursor = null, int? limit = null)
        {
            Cursor = cursor;
            Limit = limit;
        }

        /// <summary>
        /// Gets or sets the cursor for pagination continuation.
        /// </summary>
        /// <value>The cursor.</value>
        public string Cursor { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of items to return.
        /// </summary>
        /// <value>The limit.</value>
        public int? Limit { get; set; }

        internal virtual Dictionary<string, string> ToParameters()
        {
            var parameters = new Dictionary<string, string>
            {
                { "cursor", Cursor },
                { "limit", Limit?.ToString() }
            };

            return parameters.ToNonEmptyValuesDictionary();
        }
    }
}
