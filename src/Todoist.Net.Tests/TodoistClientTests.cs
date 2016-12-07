using Todoist.Net.Tests.Settings;

using Xunit;

namespace Todoist.Net.Tests
{
    public class TodoistClientTests
    {
        [Fact]
        public void GetAllResources_Success()
        {
            ITodoistClient client = new TodoistClient(SettingsProvider.GetToken());

            var resources = client.GetResourcesAsync().Result;
        }
    }
}
