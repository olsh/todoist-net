using System.Linq;
using System.Threading.Tasks;

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
        public async Task GetNotification_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var notifications = await client.Notifications.GetAsync();

            Assert.True(notifications.Any());
        }

        [Fact]
        public async Task MarkAllAsRead_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            await client.Notifications.MarkAllReadAsync();
        }
    }
}
