using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for reminders management.
    /// </summary>
    internal class RemindersService : RemindersCommandService, IRemindersService
    {
        internal RemindersService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<SyncResponse<Reminder>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncResourceAsync(new[] { ResourceType.Reminders, ResourceType.RemindersLocation }, r => r.Reminders, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<SyncResponse<string[]>> SyncLocationsAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncResourceAsync(ResourceType.Locations, r => r.Locations, syncToken, cancellationToken);
        }
    }
}
