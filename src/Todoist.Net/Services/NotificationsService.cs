using System.Collections.Generic;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    internal class NotificationsService : NotificationsCommandService, INotificationsService
    {
        public NotificationsService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        public NotificationsService(ICollection<Command> queue)
            : base(queue)
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Notification>> GetAsync()
        {
            var response = await TodoistClient.GetResourcesAsync(ResourceType.Notifications).ConfigureAwait(false);

            return response.Notifications;
        }
    }
}
