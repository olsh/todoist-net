using System;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for users management.
    /// </summary>
    public interface IUsersService : IUsersCommandService
    {
        /// <summary>
        /// Deletes the current user.
        /// </summary>
        /// <param name="userPassword">The user password.</param>
        /// <param name="reason">The reason.</param>
        /// <returns>The label info.</returns>
        /// <exception cref="ArgumentNullException">API exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(string userPassword, string reason = null);

        /// <summary>
        /// Gets the current user info.
        /// </summary>
        /// <returns>The current user info.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<UserInfo> GetCurrentAsync();
    }
}