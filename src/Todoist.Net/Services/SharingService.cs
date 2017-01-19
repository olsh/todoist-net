namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for sharing management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.SharingCommandService" />
    internal class SharingService : SharingCommandService, ISharingService
    {
        public SharingService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }
    }
}
