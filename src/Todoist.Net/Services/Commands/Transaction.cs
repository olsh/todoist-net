using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Represents a Transaction
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.ITransaction" />
    internal class Transaction : ITransaction
    {
        private readonly List<Command> _commands;
        private readonly IAdvancedTodoistClient _todoistClient;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <param name="todoistClient">The client.</param>
        internal Transaction(IAdvancedTodoistClient todoistClient)
        {
            _todoistClient = todoistClient;

            _commands = new List<Command>();

            Workspaces = new WorkspacesCommandService(_commands);
            WorkspaceFilters = new WorkspaceFiltersCommandService(_commands);
            Projects = new ProjectsCommandService(_commands);
            Comments = new CommentsCommandService(_commands);
            Sections = new SectionsCommandService(_commands);
            Tasks = new TasksCommandService(_commands);
            Labels = new LabelsCommandService(_commands);
            Filters = new FiltersCommandService(_commands);
            Reminders = new RemindersCommandService(_commands);
            Users = new UsersCommandService(_commands);
            ViewOptions = new ViewOptionsCommandService(_commands);
            Sharing = new SharingCommandService(_commands);
            Notifications = new NotificationsCommandService(_commands);
        }

        public IWorkspacesCommandService Workspaces { get; }
        public IWorkspaceFiltersCommandService WorkspaceFilters { get; }
        public IProjectsCommandService Projects { get; }
        public ICommentsCommandService Comments { get; }
        public ISectionsCommandService Sections { get; }
        public ITasksCommandService Tasks { get; }
        public ILabelsCommandService Labels { get; }
        public IFiltersCommandService Filters { get; }
        public IRemindersCommandService Reminders { get; }
        public IUsersCommandService Users { get; }
        public IViewOptionsCommandService ViewOptions { get; }
        public ISharingCommandService Sharing { get; }
        public INotificationsCommandService Notifications { get; }

        /// <inheritdoc/>
        public async Task<SyncTransactionResponse> CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _todoistClient
                    .SyncCommandsAsync(_commands.ToArray(), throwOnError: false, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            }
            finally
            {
                _commands.Clear();
            }
        }

        /// <inheritdoc/>
        public async Task<SyncTransactionResponse> CommitAndSyncAsync(ResourceType[] resourceTypes, string syncToken = "*", CancellationToken cancellationToken = default)
        {
            try
            {
                return await _todoistClient
                    .SyncCommandsAsync(_commands.ToArray(), resourceTypes, syncToken, throwOnError: false, cancellationToken)
                    .ConfigureAwait(false);
            }
            finally
            {
                _commands.Clear();
            }
        }

    }
}
