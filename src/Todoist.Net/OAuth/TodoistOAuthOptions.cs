namespace Todoist.Net.OAuth
{
    /// <summary>
    /// The credentials of the Todoist application the OAuth tokens were issued to.
    /// </summary>
    public sealed class TodoistOAuthOptions
    {
        /// <summary>
        /// Gets or sets the client ID of the application.
        /// </summary>
        /// <value>The client ID of the application.</value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret of the application.
        /// </summary>
        /// <value>
        /// The client secret of the application. Public clients have none, but revoking tokens requires it.
        /// </value>
        public string ClientSecret { get; set; }
    }
}
