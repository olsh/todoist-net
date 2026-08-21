using System;

namespace Todoist.Net
{
    /// <summary>
    /// Represents the access and refresh tokens used for authenticating with the Todoist API.
    /// </summary>
    public sealed class TodoistTokens
    {
        /// <summary>
        /// Gets the access token used for authenticating API requests.
        /// </summary>
        public string AccessToken { get; }

        /// <summary>
        /// Gets the refresh token used for obtaining a new access token when the current one expires.
        /// </summary>
        public string RefreshToken { get; }

        /// <summary>
        /// Gets or sets the expiration time of the access token. This property is optional and may be null if the expiration time is not known.
        /// </summary>
        public DateTime? ExpirationTimeUtc { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistTokens"/> class with the specified access and refresh tokens.
        /// </summary>
        /// <param name="accessToken">The access token used for authenticating API requests.</param>
        /// <param name="refreshToken">The refresh token used for obtaining a new access token when the current one expires.</param>
        /// <param name="expirationTimeUtc">The expiration time of the access token in UTC. This parameter is optional and may be null if the expiration time is not known.</param>
        public TodoistTokens(string accessToken, string refreshToken, DateTime? expirationTimeUtc = null)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpirationTimeUtc = expirationTimeUtc;
        }
    }
}
