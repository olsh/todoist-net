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
        private readonly LinkedList<Command> _commands;

        private readonly IAdvancedTodoistClient _todoistClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <param name="todoistClient">The client.</param>
        internal Transaction(IAdvancedTodoistClient todoistClient)
        {
            _todoistClient = todoistClient;
            _commands = new LinkedList<Command>();

            Project = new ProjectsCommandService(_commands);
            Comments = new CommentsCommandService(_commands);
            Tasks = new TasksCommandService(_commands);
            Labels = new LabelsCommandService(_commands);
            Notifications = new NotificationsCommandService(_commands);
            Filters = new FiltersCommandService(_commands);
            Reminders = new RemindersCommandService(_commands);
            Sections = new SectionsCommandService(_commands);
            Users = new UsersCommandService(_commands);
            Sharing = new SharingCommandService(_commands);
        }

        public IFiltersCommandService Filters { get; }

        public ITasksCommandService Tasks { get; }

        public ILabelsCommandService Labels { get; }

        public ICommentsCommandService Comments { get; }

        public INotificationsCommandService Notifications { get; }

        public IProjectCommandService Project { get; }

        public IRemindersCommandService Reminders { get; }

        public ISectionsCommandService Sections { get; }

        public ISharingCommandService Sharing { get; }

        public IUsersCommandService Users { get; }

        /// <inheritdoc/>
        public async Task<string> CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _todoistClient.ExecuteCommandsAsync(cancellationToken, _commands.ToArray()).ConfigureAwait(false);
            }
            finally
            {
                _commands.Clear();
            }
        }
    }
}
