using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net
{
    /// <summary>
    /// A Todoist client without an access token.
    /// </summary>
    public interface ITodoistTokenlessClient
    {
        /// <summary>
        /// Logins user and returns a new instance of Todoist client.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A new instance of Todoist client.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Value cannot be null or empty. - email
        /// or
        /// Value cannot be null or empty. - password
        /// </exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Unable to get token.</exception>
        Task<TodoistClient> LoginAsync(string email, string password, CancellationToken cancellationToken = default);

        /// <summary>
        /// Logins user and returns a new instance of Todoist client.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="oauthToken">The oauth token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A new instance of Todoist client.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Value cannot be null or empty. - email
        /// or
        /// Value cannot be null or empty. - oauthToken
        /// </exception>
        /// <exception cref="TodoistException">API exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<TodoistClient> LoginWithGoogleAsync(string email, string oauthToken, CancellationToken cancellationToken = default);

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The user info.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="user" /> is <see langword="null" /></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<UserInfo> RegisterUserAsync(UserBase user, CancellationToken cancellationToken = default);
    }
}
