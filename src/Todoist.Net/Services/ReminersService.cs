using System.Collections.Generic;
using System.Net.Http;
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

        /// <summary>
        /// Gets all reminders.
        /// </summary>
        /// <returns>The filters.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task<IEnumerable<Reminder>> GetAsync()
        {
            var response = await TodoistClient.GetResourcesAsync(ResourceType.Reminders).ConfigureAwait(false);

            return response.Reminders;
        }

        /// <summary>
        /// Gets a reminder info by ID.
        /// </summary>
        /// <param name="id">The ID of the reminder.</param>
        /// <returns>
        /// The reminder info.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task<ReminderInfo> GetAsync(ComplexId id)
        {
            return TodoistClient.PostAsync<ReminderInfo>(
                "reminders/get",
                new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>(
                            "reminder_id",
                            id.ToString())
                    });
        }
    }
}
