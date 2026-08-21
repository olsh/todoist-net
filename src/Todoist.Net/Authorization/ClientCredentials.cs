namespace Todoist.Net
{
    /// <summary>
    /// Represents the properties of a client application used for authenticating with the Todoist API.
    /// </summary>
    public sealed class ClientCredentials
    {
        /// <summary>
        /// Gets the client ID of the application.
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// Gets the client secret of the application.
        /// </summary>
        public string ClientSecret { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ClientCredentials"/> class with the specified client ID and client secret.
        /// </summary>
        /// <param name="clientId">The client ID of the application.</param>
        /// <param name="clientSecret">The client secret of the application.</param>
        public ClientCredentials(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }
    }
}
