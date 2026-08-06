using System.Threading;
using System.Threading.Tasks;

namespace Todoist.Net
{
    /// <summary>
    /// Represents a Todoist REST client that supports token refreshing and revocation.
    /// </summary>
    public interface IRefreshableTodoistRestClient : ITodoistRestClient
    {
        /// <summary>
        /// Refreshes the access token using the refresh token and updates the internal state of the client.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        Task<TokenRefreshResponse> RefreshTokensAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Revokes the current access token and refresh token, effectively logging the user out and invalidating the tokens.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        Task RevokeTokensAsync(CancellationToken cancellationToken = default);
    }
}
