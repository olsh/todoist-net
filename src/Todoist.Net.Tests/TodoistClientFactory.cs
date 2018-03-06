using Todoist.Net.Tests.Settings;

namespace Todoist.Net.Tests
{
    public static class TodoistClientFactory
    {
        public static ITodoistClient Create()
        {
            var token = SettingsProvider.GetToken();
            return new TodoistClient(token, new RateLimitAwareRestClient());
        }

        public static ITodoistTokenlessClient CreateTokenlessClient()
        {
            return new TodoistTokenlessClient();
        }
    }
}
