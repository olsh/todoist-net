using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal class UsersCommandService : CommandServiceBase, IUsersCommandService
    {
        internal UsersCommandService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        internal UsersCommandService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <inheritdoc/>
        public Task UpdateAsync(UpdateUser user, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateUser, user, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateKarmaGoalsAsync(UpdateKarmaGoals karmaGoals, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateKarmaGoals, karmaGoals, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateSettingsAsync(UpdateUserSettings settings, CancellationToken cancellationToken = default)
        {
            return ExecuteCommandAsync(CommandType.UpdateUserSettings, settings, cancellationToken);
        }
    }
}
