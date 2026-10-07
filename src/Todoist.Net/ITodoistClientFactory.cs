namespace Todoist.Net
{
    /// <summary>
    /// A factory abstraction for a component that can create <see cref="ITodoistClient"/> instances with user tokens.
    /// </summary>
    public interface ITodoistClientFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="ITodoistClient"/> with the specified user token."/>
        /// </summary>
        /// <param name="token">The user token to use.</param>
        /// <returns>The created <see cref="ITodoistClient"/></returns>
        ITodoistClient CreateClient(string token);
    }
}
