using System;
using System.Collections.Generic;
using System.Net.Http;
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

        /// <summary>
        /// Gets the current user info.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The current user info.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="user" /> is <see langword="null" /></exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        public Task UpdateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var command = new Command(CommandType.UpdateUser, user);

            return ExecuteCommandAsync(command);
        }

        /// <summary>
        /// Gets the current user info.
        /// </summary>
        /// <param name="karmaGoals">The karma goals.</param>
        /// <returns>The current user info.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="karmaGoals" /> is <see langword="null" /></exception>
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
