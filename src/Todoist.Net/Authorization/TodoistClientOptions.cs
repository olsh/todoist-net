using System;
using System.Threading;
using System.Threading.Tasks;

namespace Todoist.Net
{
    /// <summary>
    /// Represents the properties of a client application used for authenticating with the Todoist API.
    /// </summary>
    public sealed class TodoistClientOptions
    {
        /// <summary>
        /// Gets or sets the client credentials of the application, including the client ID and client secret.
        /// </summary>
        public ClientCredentials Credentials { get; set; }

        /// <summary>
        /// Gets or sets the callback to invoke when the tokens are refreshed.
        /// </summary>
        public Func<IServiceProvider, TokenRefreshResponse, object, CancellationToken, Task> OnRefresh { get; set; }
    }
}
