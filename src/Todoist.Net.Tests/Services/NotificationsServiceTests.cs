using System.Linq;

using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
    public class NotificationsServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public NotificationsServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void GetNotification_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var notifications = client.Notifications.GetAsync().Result;

            Assert.True(notifications.Any());
        }

        [Fact]
        public void MarkAllAsRead_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            client.Notifications.MarkAllReadAsync().Wait();
        }
    }
}
