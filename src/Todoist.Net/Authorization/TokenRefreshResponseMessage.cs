using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Todoist.Net
{
    /// <summary>
    /// Represents the response from an OAuth token refresh request, encapsulating the HTTP status, headers, and parsed token refresh content.
    /// </summary>
    public sealed class TokenRefreshResponseMessage
    {
        /// <inheritdoc cref="HttpResponseMessage.IsSuccessStatusCode"/>
        public bool IsSuccessStatusCode => (int)StatusCode >= 200 && (int)StatusCode <= 299;

        /// <inheritdoc cref="HttpResponseMessage.StatusCode"/>
        public HttpStatusCode StatusCode { get; }

        /// <inheritdoc cref="HttpResponseMessage.Headers"/>
        public HttpResponseHeaders Headers { get; }

        /// <summary>
        /// Gets the content of the token refresh response, which contains the new access token, refresh token, and other related information.
        /// </summary>
        public TokenRefreshResponse Content { get; }

        internal TokenRefreshResponseMessage(HttpStatusCode statusCode, HttpResponseHeaders headers, TokenRefreshResponse content = null)
        {
            StatusCode = statusCode;
            Headers = headers;
            Content = content;
        }

        internal static async Task<TokenRefreshResponseMessage> FromHttpResponseMessageAsync(HttpResponseMessage httpResponseMessage, CancellationToken cancellationToken = default)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return new TokenRefreshResponseMessage(httpResponseMessage.StatusCode, httpResponseMessage.Headers);
            }
            using (var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
            {
                var content = await JsonSerializer.DeserializeAsync<TokenRefreshResponse>(contentStream, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                return new TokenRefreshResponseMessage(httpResponseMessage.StatusCode, httpResponseMessage.Headers, content);
            }
        }

        /// <inheritdoc cref="HttpResponseMessage.EnsureSuccessStatusCode"/>
        public TokenRefreshResponseMessage EnsureSuccessStatusCode()
        {
            if (IsSuccessStatusCode)
            {
                return this;
            }
            throw new HttpRequestException($"Response status code does not indicate success: {(int)StatusCode} ({StatusCode}).");
        }
    }
}
