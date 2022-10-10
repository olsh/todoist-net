using System;
using System.Threading.Tasks;

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
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task GetCurrentAsync_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var user = await client.Users.GetCurrentAsync();

            Assert.NotNull(user);
            Assert.True(user.Id > 0);
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task RegisterUpdateSettingsAndDeleteUser_Success()
        {
            var todoistTokenlessClient = TodoistClientFactory.CreateTokenlessClient();

            const string password = "Pa$$W@rd";
            var userBase = new UserBase(Guid.NewGuid().ToString("N") + "@example.com", "test user", password);
            var userInfo = await todoistTokenlessClient.RegisterUserAsync(userBase);
            Assert.NotNull(userInfo);

            var todoistClient = await todoistTokenlessClient.LoginAsync(userBase.Email, userBase.Password);
            await todoistClient.Users.UpdateKarmaGoalsAsync(new KarmaGoals() { KarmaDisabled = true });

            if (userInfo.HasPassword)
            {
                userInfo.CurrentPassword = password;
            }

            await todoistClient.Users.UpdateAsync(userInfo);

            await todoistClient.Users.DeleteAsync(userBase.Password, "test");

            todoistClient.Dispose();
        }
    }
}
