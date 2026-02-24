using System.Collections.Generic;
using System.Linq;
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
        private string _syncToken;
        private readonly HashSet<ResourceType> _includedResources;
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
            _includedResources = new HashSet<ResourceType>();

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
        public void IncludeResources(ResourceType[] resourceTypes, string syncToken = null)
        {
            if (resourceTypes?.Length > 0)
            {
                foreach (var resourceType in resourceTypes)
                {
                    _includedResources.Add(resourceType);
                }
            }
            _syncToken = syncToken;
        }

        /// <inheritdoc/>
        public async Task<SyncTransactionResponse> CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _todoistClient
                    .SyncCommandsAsync(_commands.ToArray(), _includedResources.ToArray(), _syncToken, throwOnError: false, cancellationToken)
                    .ConfigureAwait(false);
            }
            finally
            {
                _commands.Clear();
            }
        }
    }
}
