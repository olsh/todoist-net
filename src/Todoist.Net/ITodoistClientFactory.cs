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
        /// <param name="token">The user token to use.</param>
        /// <returns>The created <see cref="TodoistClient"/></returns>
        TodoistClient CreateClient(string token);
    }
}
