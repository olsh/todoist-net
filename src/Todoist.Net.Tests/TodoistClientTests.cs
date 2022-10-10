using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    [IntegrationFree]
    public class TodoistClientTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public TodoistClientTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void GetAllResources_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var resources = client.GetResourcesAsync().Result;

            Assert.NotNull(resources);
        }
    }
}
