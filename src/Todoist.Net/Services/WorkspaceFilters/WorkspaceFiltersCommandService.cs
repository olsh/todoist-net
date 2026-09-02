using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for workspace filters management which can be executed in a transaction.
    /// </summary>
    internal class WorkspaceFiltersCommandService : CommandServiceBase, IWorkspaceFiltersCommandService
    {
        internal WorkspaceFiltersCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal WorkspaceFiltersCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <inheritdoc/>
        public Task<ComplexId> AddAsync(AddWorkspaceFilter workspaceFilter, CancellationToken cancellationToken = default)
        {
            return ExecuteAddCommandAsync(CommandType.AddWorkspaceFilter, workspaceFilter, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(UpdateWorkspaceFilter workspaceFilter, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateWorkspaceFilter, workspaceFilter, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return ExecuteEntityCommandAsync(CommandType.DeleteWorkspaceFilter, id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateOrdersAsync(UpdateWorkspaceFilterOrders workspaceFilterOrders, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateWorkspaceFilterOrders, workspaceFilterOrders, cancellationToken);
        }
    }
}
