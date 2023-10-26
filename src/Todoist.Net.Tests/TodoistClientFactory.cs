using Todoist.Net.Tests.Settings;

using Xunit.Abstractions;

namespace Todoist.Net.Tests
{
    public static class TodoistClientFactory
    {
        public static ITodoistClient Create(ITestOutputHelper outputHelper)
        {
            var token = SettingsProvider.GetToken();
            return new TodoistClient(new RateLimitAwareRestClient(token, outputHelper));
        }
    }
}
