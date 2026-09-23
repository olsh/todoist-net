using System.Threading.Tasks;

using Todoist.Net.Exceptions;

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
        /// Gets or sets the optional state object to pass to the refresh handler. This can be used to maintain context or additional information needed during the token refresh process.
        /// </summary>
        public object RefreshState { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether automatic token refresh is disabled. If set to true, the tokens will not be refreshed automatically.
        /// </summary>
        public bool DisableAutomaticRefresh { get; set; }


        /// <inheritdoc cref="TodoistAuthenticationContext(ClientCredentials, TodoistTokens, TokenRefreshHandler, bool)"/>
        public TodoistAuthenticationContext(ClientCredentials credentials, TodoistTokens tokens, bool disableAutomaticRefresh = false)
            : this(credentials, tokens, null, disableAutomaticRefresh) { }

        /// <inheritdoc cref="TodoistAuthenticationContext(ClientCredentials, TodoistTokens, TokenRefreshHandler, object, bool)"/>
        public TodoistAuthenticationContext(ClientCredentials credentials, TodoistTokens tokens, TokenRefreshHandler onRefresh, bool disableAutomaticRefresh = false)
            : this(credentials, tokens, onRefresh, null, disableAutomaticRefresh) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistAuthenticationContext"/> class.
        /// </summary>
        /// <param name="credentials">The client credentials of the application, including the client ID and client secret.</param>
        /// <param name="tokens">The user tokens, including the access token and refresh token, used for authenticating with the Todoist API.</param>
        /// <param name="onRefresh">The callback to invoke when the tokens are refreshed.</param>
        /// <param name="refreshState">A state object to pass to the refresh handler.</param>
        /// <param name="disableAutomaticRefresh">A value indicating whether automatic token refresh is disabled. If set to true, the tokens will not be refreshed automatically.</param>
        public TodoistAuthenticationContext(ClientCredentials credentials, TodoistTokens tokens, TokenRefreshHandler onRefresh, object refreshState, bool disableAutomaticRefresh = false)
        {
            ThrowHelper.ThrowIfNull(credentials, nameof(credentials));
            ThrowHelper.ThrowIfNull(tokens, nameof(tokens));

            Credentials = credentials;
            Tokens = tokens;

            OnRefresh = onRefresh ?? new TokenRefreshHandler((res, state, ct) => Task.CompletedTask);
            RefreshState = refreshState;

            DisableAutomaticRefresh = disableAutomaticRefresh;
        }
    }
}
