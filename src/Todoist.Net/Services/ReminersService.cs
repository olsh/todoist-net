using System.Collections.Generic;
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
        public async Task<IEnumerable<Reminder>> GetAsync(CancellationToken cancellationToken = default)
        {
            var response = await TodoistClient.GetResourcesAsync(cancellationToken, ResourceType.Reminders).ConfigureAwait(false);

            return response.Reminders;
        }

        /// <inheritdoc/>
        public Task<ReminderInfo> GetAsync(ComplexId id, CancellationToken cancellationToken = default)
        {
            return TodoistClient.PostAsync<ReminderInfo>(
                "reminders/get",
                new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>(
                            "reminder_id",
                            id.ToString())
                    },
                cancellationToken);
        }
    }
}
