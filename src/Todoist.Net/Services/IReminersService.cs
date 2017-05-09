using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for reminders management.
    /// </summary>
    public interface IRemindersService : IRemindersCommandService
    {
        /// <summary>
        /// Gets all reminders.
        /// </summary>
        /// <returns>The filters.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<IEnumerable<Reminder>> GetAsync();

        /// <summary>
        /// Gets a reminder info by ID.
        /// </summary>
        /// <param name="id">The ID of the reminder.</param>
        /// <returns>
        /// The reminder info.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ReminderInfo> GetAsync(ComplexId id);
    }
}
