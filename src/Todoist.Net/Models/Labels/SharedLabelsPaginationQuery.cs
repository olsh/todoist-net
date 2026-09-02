using System.Collections.Generic;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a paginated search query with common pagination parameters and a filter string for search criteria.
    /// </summary>
    public class SharedLabelsPaginationQuery : PaginationQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharedLabelsPaginationQuery" /> class.
        /// </summary>
        /// <param name="omitPersonal">Whether to omit personal labels.</param>
        /// <param name="cursor">The cursor for pagination continuation.</param>
        /// <param name="limit">The maximum number of items to return.</param>
        public SharedLabelsPaginationQuery(bool omitPersonal = false, string cursor = null, int? limit = null)
            : base(cursor, limit)
        {
            OmitPersonal = omitPersonal;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to omit personal labels.
        /// </summary>
        public bool OmitPersonal { get; set; }

        internal override Dictionary<string, string> ToParameters()
        {
            var parameters = base.ToParameters();
            
            parameters.AddIfTrue("omit_personal", OmitPersonal);

            return parameters;
        }
    }
}
