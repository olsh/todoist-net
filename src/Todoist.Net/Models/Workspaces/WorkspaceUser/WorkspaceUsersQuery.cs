using System.Collections.Generic;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents optional query parameters for workspace users listing.
    /// </summary>
    public class WorkspaceUsersQuery : PaginationQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceUsersQuery" /> class.
        /// </summary>
        /// <param name="workspaceId">The ID of the workspace to filter users by.</param>
        /// <param name="cursor">The cursor for pagination continuation.</param>
        /// <param name="limit">The maximum number of items to return.</param>
        public WorkspaceUsersQuery(long? workspaceId = null, string cursor = null, int? limit = null)
            : base(cursor, limit)
        {
            WorkspaceId = workspaceId;
        }

        /// <summary>
        /// Gets or sets the optional workspace ID to scope users by workspace.
        /// </summary>
        public long? WorkspaceId { get; set; }

        internal override Dictionary<string, string> ToParameters()
        {
            var parameters = base.ToParameters();

            parameters.AddIfHasValue("workspace_id", WorkspaceId);

            return parameters;
        }
    }
}
