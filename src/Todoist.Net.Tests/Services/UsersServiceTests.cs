using System;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    public class UsersServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public UsersServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        [IntegrationFree]
        public void GetCurrentAsync_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var user = client.Users.GetCurrentAsync().Result;

            Assert.NotNull(user);
            Assert.True(user.Id > 0);
        }

        [Fact]
        [IntegrationFree]
        public void RegisterUpdateSettingsAndDeleteUser_Success()
        {
            var todoistTokenlessClient = TodoistClientFactory.CreateTokenlessClient();
            var userBase = new UserBase(Guid.NewGuid().ToString("N") + "@example.com", "test user", "Pa$$W@rd");
            var userInfo = todoistTokenlessClient.RegisterUserAsync(userBase).Result;
            Assert.NotNull(userInfo);

            var todoistClient = todoistTokenlessClient.LoginAsync(userBase.Email, userBase.Password).Result;

            todoistClient.Users.UpdateNotificationSettingsAsync(
                NotificationType.ItemCompleted,
                NotificationService.Email,
                true);
            todoistClient.Users.UpdateKarmaGoalsAsync(new KarmaGoals() { KarmaDisabled = true })
                .Wait();
            todoistClient.Users.UpdateAsync(userInfo)
                .Wait();

            todoistClient.Users.DeleteAsync(userBase.Password, "test");

            todoistClient.Dispose();
        }
    }
}
