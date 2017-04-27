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
    }
}
