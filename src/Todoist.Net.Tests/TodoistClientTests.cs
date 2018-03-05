using Xunit;

namespace Todoist.Net.Tests
{
    public class TodoistClientTests
    {
        [Fact]
        public void GetAllResources_Success()
        {
            var client = TodoistClientFactory.Create();

            var resources = client.GetResourcesAsync().Result;

            Assert.NotNull(resources);
        }
    }
}
