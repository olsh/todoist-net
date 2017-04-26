using System.Linq;

using Todoist.Net.Tests.Settings;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class NotificationsServiceTests
    {
        [Fact]
        public void GetNotification_Success()
        {
            var client = TodoistClientFactory.Create();

            var notifications = client.Notifications.GetAsync().Result;

            Assert.True(notifications.Any());
        }

        [Fact]
        public void MarkAllAsRead_Success()
        {
            var client = TodoistClientFactory.Create();

            client.Notifications.MarkAllReadAsync().Wait();
        }
    }
}
