using System;
using System.Collections.Generic;
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
        public Task UpdateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var command = new Command(CommandType.UpdateUser, user);

            return ExecuteCommandAsync(command);
        }

        /// <inheritdoc/>
        public Task UpdateKarmaGoalsAsync(KarmaGoals karmaGoals)
        {
            if (karmaGoals == null)
            {
                throw new ArgumentNullException(nameof(karmaGoals));
            }

            var command = new Command(CommandType.UpdateKarmaGoals, karmaGoals);

            return ExecuteCommandAsync(command);
        }
    }
}
