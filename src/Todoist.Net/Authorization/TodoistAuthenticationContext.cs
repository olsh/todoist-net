using System.Threading.Tasks;

namespace Todoist.Net
{
    /// <summary>
    /// Represents the authentication context for a Todoist client, including client credentials, user tokens, and an optional token refresh handler.
    /// </summary>
    public sealed class TodoistAuthenticationContext
    {
        /// <summary>
        /// Gets or sets the client credentials of the application, including the client ID and client secret.
        /// </summary>
        public ClientCredentials Credentials { get; set; }

        /// <summary>
        /// Gets or sets the user tokens, including the access token and refresh token, used for authenticating with the Todoist API.
        /// </summary>
        public TodoistTokens Tokens { get; set; }

        /// <summary>
        /// Gets or sets the optional callback to invoke when the tokens are refreshed. This allows the application to handle token updates, such as storing the new tokens securely.
        /// </summary>
        public TokenRefreshHandler OnRefresh { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistAuthenticationContext"/> class with the specified client credentials, user tokens, and an optional token refresh handler.
        /// </summary>
        /// <param name="credentials">The client credentials of the application, including the client ID and client secret.</param>
        /// <param name="tokens">The user tokens, including the access token and refresh token, used for authenticating with the Todoist API.</param>
        /// <param name="onRefresh">The optional callback to invoke when the tokens are refreshed.</param>
        public TodoistAuthenticationContext(ClientCredentials credentials, TodoistTokens tokens, TokenRefreshHandler onRefresh = null)
        {
            Credentials = credentials;
            Tokens = tokens;

            OnRefresh = onRefresh ?? new TokenRefreshHandler((res, ct) => Task.CompletedTask);
        }
    }
}
