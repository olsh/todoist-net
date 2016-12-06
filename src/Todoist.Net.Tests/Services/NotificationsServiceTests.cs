using System.Linq;

using Todoist.Net.Tests.Settings;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class NotificationsServiceTests
    {
        [Fact]
        public void GetNotificationAndMarkRead_Success()
        {
            var client = CreateClient();

            var notifications = client.Notifications.GetAsync().Result;

            Assert.True(notifications.Count() > 0);
        }

        [Fact]
        public void MarkAllAsRead_Success()
        {
            var client = CreateClient();

            client.Notifications.MarkAllReadAsync().Wait();
        }

        private static ITodoistClient CreateClient()
        {
            var client = new TodoistClient(SettingsProvider.GetToken());
            return client;
        }
    }
}