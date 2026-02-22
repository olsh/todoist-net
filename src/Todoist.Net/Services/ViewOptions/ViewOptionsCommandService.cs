using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal class ViewOptionsCommandService : CommandServiceBase, IViewOptionsCommandService
    {
        internal ViewOptionsCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal ViewOptionsCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <inheritdoc/>
        public Task SetAsync(ViewOptions viewOptions, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.SetViewOptions, viewOptions, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ViewOptions viewOptions, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.DeleteViewOptions, viewOptions, cancellationToken);
        }

        /// <inheritdoc/>
        public Task SetProjectDefaultsAsync(ProjectViewOptionsDefaults viewDefaults, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.SetProjectViewOptionsDefaults, viewDefaults, cancellationToken);
        }
    }
}
