using System;
using System.Collections.Generic;
using System.Net.Http;
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

        /// <summary>
        /// Deletes the current user.
        /// </summary>
        /// <param name="userPassword">The user password.</param>
        /// <param name="reason">The reason.</param>
        /// <returns>The label info.</returns>
        /// <exception cref="ArgumentNullException">API exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task DeleteAsync(string userPassword, string reason = null)
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

            return TodoistClient.PostRawAsync("user/delete", parameters);
        }

        /// <summary>
        /// Gets the current user info.
        /// </summary>
        /// <returns>The current user info.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task<UserInfo> GetCurrentAsync()
        {
            var response = await TodoistClient.GetResourcesAsync(ResourceType.User).ConfigureAwait(false);

            return response.UserInfo;
        }
    }
}
