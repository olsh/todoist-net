using System;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class UsersServiceTests
    {
        [Fact]
        [IntegrationFree]
        public void GetCurrentAsync_Success()
        {
            var client = TodoistClientFactory.Create();

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

#pragma warning disable CS0618 // Type or member is obsolete
            var todoistClient = todoistTokenlessClient.LoginAsync(userBase.Email, userBase.Password).Result;
#pragma warning restore CS0618 // Type or member is obsolete

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
