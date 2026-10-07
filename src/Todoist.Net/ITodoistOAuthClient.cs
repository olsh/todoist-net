using System;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.OAuth;

namespace Todoist.Net
{
    /// <summary>
    /// A Todoist OAuth client.
    /// </summary>
    public interface ITodoistOAuthClient : ITodoistClient
    {
        /// <summary>
        /// Refreshes the OAuth tokens ahead of their expiration.
        /// </summary>
        /// <remarks>
        /// The client refreshes the tokens on its own when they expire or get rejected, so calling this method is optional.
        /// The refreshed tokens are passed to the callback given when the client was created, the same way as after an automatic refresh.
        /// </remarks>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The refreshed tokens.</returns>
        /// <exception cref="InvalidOperationException">The client was not created with OAuth tokens, or they include no refresh token.</exception>
        /// <exception cref="TodoistException">Todoist rejected the refresh, e.g. because the refresh token was revoked.</exception>
        Task<TodoistTokens> RefreshTokensAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Revokes the OAuth access token, and stops the client from refreshing the tokens.
        /// </summary>
        /// <remarks>
        /// <para>Revoking requires the client secret of the application.</para>
        /// <para>
        /// Todoist can revoke access tokens only, so the refresh token keeps working: delete the stored tokens to give up
        /// the access for good. The authorization itself ends when the user removes the application in the Todoist settings.
        /// </para>
        /// </remarks>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">The client was not created with OAuth tokens, or without the client secret.</exception>
        Task RevokeTokensAsync(CancellationToken cancellationToken = default);
    }
}
