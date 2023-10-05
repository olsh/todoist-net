using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net
{
    /// <summary>
    /// A Todoist client without an access token.
    /// </summary>
    /// <seealso cref="Todoist.Net.ITodoistTokenlessClient" />
    /// <seealso cref="System.IDisposable" />
    public sealed class TodoistTokenlessClient : ITodoistTokenlessClient, IDisposable
    {
        private readonly TodoistClient _todoistClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistTokenlessClient" /> class.
        /// </summary>
        public TodoistTokenlessClient() : this(new TodoistRestClient())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistTokenlessClient" /> class.
        /// </summary>
        /// <param name="proxy">The proxy.</param>
        public TodoistTokenlessClient(IWebProxy proxy) : this(new TodoistRestClient(proxy))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistTokenlessClient" /> class.
        /// </summary>
        /// <param name="restClient">The rest client.</param>
        public TodoistTokenlessClient(ITodoistRestClient restClient)
        {
            _todoistClient = new TodoistClient(restClient);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _todoistClient?.Dispose();
        }

        /// <inheritdoc/>
        [Obsolete("This method is scheduled for deprecation and probably will be removed in future versions.")]
        public Task<TodoistClient> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(email));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(password));
            }

            var parameters = new[]
                                 {
                                     new KeyValuePair<string, string>("email", email),
                                     new KeyValuePair<string, string>("password", password)
                                 };

            return LoginWithCredentialsAsync("login", parameters, cancellationToken);
        }

        /// <inheritdoc/>
        [Obsolete("This method is scheduled for deprecation and probably will be removed in future versions.")]
        public Task<TodoistClient> LoginWithGoogleAsync(string email, string oauthToken, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(email));
            }

            if (string.IsNullOrEmpty(oauthToken))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(oauthToken));
            }

            var parameters = new[]
                                 {
                                     new KeyValuePair<string, string>("email", email),
                                     new KeyValuePair<string, string>("oauth2_token", oauthToken)
                                 };

            return LoginWithCredentialsAsync("login_with_google", parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<UserInfo> RegisterUserAsync(UserBase user, CancellationToken cancellationToken = default)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var userInfo = await ((IAdvancedTodoistClient)_todoistClient)
                               .ProcessPostAsync<UserInfo>("user/register", user.ToParameters(), cancellationToken)
                               .ConfigureAwait(false);

            return userInfo;
        }

        /// <summary>
        /// Logins with credentials and returns a new instance of Todoist client.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A new instance of Todoist client.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Unable to get token.</exception>
        private async Task<TodoistClient> LoginWithCredentialsAsync(
            string resource,
            KeyValuePair<string, string>[] parameters,
            CancellationToken cancellationToken)
        {
            var userInfo = await ((IAdvancedTodoistClient)_todoistClient)
                               .ProcessPostAsync<UserInfo>(resource, parameters, cancellationToken)
                               .ConfigureAwait(false);

            return new TodoistClient(userInfo.Token);
        }
    }
}
