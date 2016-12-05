using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    public class Transaction
    {
        private readonly LinkedList<Command> _commands;

        private readonly ITodoistClient _todoistClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <param name="todoistClient">The client.</param>
        internal Transaction(ITodoistClient todoistClient)
        {
            _todoistClient = todoistClient;
            _commands = new LinkedList<Command>();

            Project = new ProjectService(_commands);
            Notes = new NotesServices(_commands);
        }

        public INotesCommandServices Notes { get; }

        public IProjectCommandService Project { get; }

        /// <summary>
        /// Commits the transaction asynchronous.
        /// </summary>
        /// <returns>The task.</returns>
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
