using System.Threading.Tasks;

using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
    public class TodoistClientTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public TodoistClientTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task GetAllResources_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var resources = await client.GetResourcesAsync();

            Assert.NotNull(resources);
        }

        [Fact]
        public async Task GetAllResourcesWithSyncToken_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var resources = await client.GetResourcesAsync();
            resources = await client.GetResourcesAsync(resources.SyncToken);

            Assert.NotNull(resources);
        }
    }
}
