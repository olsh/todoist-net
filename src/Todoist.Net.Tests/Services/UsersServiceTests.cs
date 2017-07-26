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
        }

        [Fact]
        public void RegisterUpdateSettingsAndDeleteUser_Success()
        {
            var userBase = new UserBase(Guid.NewGuid().ToString("N") + "@example.com", "test user", "Pa$$W@rd");
            var userInfo = TodoistClient.RegisterUserAsync(userBase).Result;
            Assert.NotNull(userInfo);

            var todoistClient = TodoistClient.LoginAsync(userBase.Email, userBase.Password).Result;
            todoistClient.Users.UpdateNotificationSettingsAsync(
                NotificationType.ItemCompleted,
                NotificationService.Email,
                true);
            todoistClient.Users.DeleteAsync(userBase.Password, "test");

            todoistClient.Dispose();
        }
    }
}
