namespace Todoist.Net
{
    /// <summary>
    /// A factory abstraction for a component that can create <see cref="TodoistClient"/> instances with user tokens.
    /// </summary>
    public interface ITodoistClientFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="TodoistClient"/> with the specified user token."/>
        /// </summary>
        /// <param name="legacyToken">The legacy user token to use.</param>
        /// <returns>The created <see cref="TodoistClient"/></returns>
        TodoistClient CreateClient(string legacyToken);

        /// <summary>
        /// Creates a new instance of <see cref="TodoistClient"/> with the specified user tokens.
        /// </summary>
        /// <param name="tokens">The user access and refresh tokens to use.</param>
        /// <param name="refreshState">An optional state object to pass to the refresh callback.</param>
        /// <returns>The created <see cref="TodoistClient"/></returns>
        TodoistClient CreateClient(TodoistTokens tokens, object refreshState = null);
    }
}
