using System.Text.Json.Serialization;

namespace Todoist.Net
{
    /// <summary>
    /// Represents the response received after refreshing an access token.
    /// </summary>
    public sealed class TokenRefreshResponse
    {
        /// <summary>
        /// Gets the new access token.
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; }

        /// <summary>
        /// Gets the new refresh token.
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; }

        /// <summary>
        /// Gets the type of the token (usually "Bearer").
        /// </summary>
        [JsonPropertyName("token_type")]
        public string TokenType { get; }

        /// <summary>
        /// Gets the scope of the access token.
        /// </summary>
        [JsonPropertyName("scope")]
        public string Scope { get; }

        /// <summary>
        /// Gets the number of seconds until the access token expires.
        /// </summary>
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenRefreshResponse"/> class.
        /// </summary>
        /// <param name="accessToken">The new access token.</param>
        /// <param name="refreshToken">The new refresh token.</param>
        /// <param name="tokenType">The type of the token (usually "Bearer").</param>
        /// <param name="scope">The scope of the access token.</param>
        /// <param name="expiresIn">The number of seconds until the access token expires.</param>
        [JsonConstructor]
        public TokenRefreshResponse(string accessToken, string refreshToken, string tokenType, string scope, int expiresIn)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            TokenType = tokenType;
            Scope = scope;
            ExpiresIn = expiresIn;
        }
    }
}
