using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for users management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.LabelsCommandService" />
    /// <seealso cref="Todoist.Net.Services.ILabelsService" />
    internal class UsersService : UsersCommandService, IUsersService
    {
        internal UsersService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task DeleteAsync(string userPassword, string reason = null, CancellationToken cancellationToken = default)
        {
            if (userPassword == null)
            {
                throw new ArgumentNullException(nameof(userPassword));
            }

            var parameters = new LinkedList<KeyValuePair<string, string>>();
            parameters.AddLast(new KeyValuePair<string, string>("current_password", userPassword));

            if (!string.IsNullOrEmpty(reason))
            {
                parameters.AddLast(new KeyValuePair<string, string>("reason_for_delete", reason));
            }

            return TodoistClient.PostRawAsync("user/delete", parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<UserInfo> GetCurrentAsync(CancellationToken cancellationToken = default)
        {
            var response = await TodoistClient.GetResourcesAsync(cancellationToken, ResourceType.User).ConfigureAwait(false);

            return response.UserInfo;
        }
    }
}
