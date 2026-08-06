using System.Threading;
using System.Threading.Tasks;

namespace Todoist.Net
{
    /// <summary>
    /// Represents a delegate that handles the token refresh event.
    /// </summary>
    /// <param name="response">The response containing the new tokens.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public delegate Task TokenRefreshHandler(TokenRefreshResponse response, CancellationToken cancellationToken);
}
