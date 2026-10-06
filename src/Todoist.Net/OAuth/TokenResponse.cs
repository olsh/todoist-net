using System.Text.Json.Serialization;

namespace Todoist.Net.OAuth
{
    /// <summary>
    /// The body of a successful response of the OAuth token endpoint.
    /// </summary>
    internal sealed class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
