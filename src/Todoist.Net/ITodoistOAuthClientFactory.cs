using System;
using System.Threading.Tasks;

using Todoist.Net.OAuth;

namespace Todoist.Net
{
    /// <summary>
    /// A factory abstraction for a component that can create <see cref="TodoistClient"/> instances with OAuth tokens.
    /// </summary>
    public interface ITodoistOAuthClientFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="TodoistClient"/> which authorizes with the OAuth tokens of a user
        /// and refreshes them when they expire or get rejected.
        /// </summary>
        /// <param name="tokens">The OAuth tokens of the user.</param>
        /// <param name="onTokensRefreshed">
        /// The callback which stores the refreshed tokens. Todoist rotates the refresh token on every refresh,
        /// so the tokens passed to it replace the stored ones. It is required when <paramref name="tokens" /> include a refresh token.
        /// </param>
        /// <returns>The created <see cref="TodoistClient"/></returns>
        /// <exception cref="InvalidOperationException">The OAuth client ID of the application is not configured.</exception>
        TodoistClient CreateClient(TodoistTokens tokens, Func<TodoistTokens, Task> onTokensRefreshed);
    }
}
