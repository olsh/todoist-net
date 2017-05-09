using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            Notes = new NotesCommandService(_commands);
            Items = new ItemsCommandService(_commands);
            Labels = new LabelsCommandService(_commands);
            Notifications = new NotificationsCommandService(_commands);
            Filters = new FiltersCommandService(_commands);
            Reminders = new RemindersCommandService(_commands);
            Users = new UsersCommandService(_commands);
            Sharing = new SharingCommandService(_commands);
        }

        public IFiltersCommandService Filters { get; set; }

        public IItemsCommandService Items { get; }

        public ILabelsCommandService Labels { get; }

        public INotesCommandServices Notes { get; }

        public INotificationsCommandService Notifications { get; }

        public IProjectCommandService Project { get; }

        public IRemindersCommandService Reminders { get; }

        public ISharingCommandService Sharing { get; }

        public IUsersCommandService Users { get; }

        /// <summary>
        /// Commits the transaction asynchronous.
        /// </summary>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="AggregateException">Command execution exception.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task CommitAsync()
        {
            try
            {
                await _todoistClient.ExecuteCommandsAsync(_commands.ToArray()).ConfigureAwait(false);
            }
            finally
            {
                _commands.Clear();
            }
        }
    }
}
