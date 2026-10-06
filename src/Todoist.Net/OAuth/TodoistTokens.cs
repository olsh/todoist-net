using System;

using Todoist.Net.Exceptions;

namespace Todoist.Net.OAuth
{
    /// <summary>
    /// The OAuth tokens a user granted to an application.
    /// </summary>
    /// <remarks>
    /// Todoist rotates the refresh token on every refresh, so whenever a client refreshes the tokens
    /// the previous refresh token stops working and the new tokens have to be stored instead.
    /// </remarks>
    public sealed class TodoistTokens
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistTokens" /> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="refreshToken">The refresh token, or <c>null</c> when the access token cannot be refreshed.</param>
        /// <param name="expiresAt">The moment the access token expires, or <c>null</c> when it is unknown.</param>
        /// <exception cref="ArgumentException">Value cannot be null or empty - accessToken</exception>
        public TodoistTokens(string accessToken, string refreshToken = null, DateTimeOffset? expiresAt = null)
        {
            ThrowHelper.ThrowIfNullOrEmpty(accessToken, nameof(accessToken));

            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresAt = expiresAt;
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <value>The access token.</value>
        public string AccessToken { get; }

        /// <summary>
        /// Gets the refresh token.
        /// </summary>
        /// <value>The refresh token, or <c>null</c> when the access token cannot be refreshed.</value>
        public string RefreshToken { get; }

        /// <summary>
        /// Gets the moment the access token expires.
        /// </summary>
        /// <value>The moment the access token expires, or <c>null</c> when it is unknown.</value>
        public DateTimeOffset? ExpiresAt { get; }
    }
}
