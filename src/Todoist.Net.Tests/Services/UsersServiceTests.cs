using System.Threading.Tasks;

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
            Assert.NotNull(user.Id);
        }
    }
}
