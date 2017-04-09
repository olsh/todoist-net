using Todoist.Net.Tests.Settings;

namespace Todoist.Net.Tests
{
    public class TodoistClientFactory
    {
        public static ITodoistClient Create()
        {
            var token = SettingsProvider.GetToken();
            return new TodoistClient(token, new RateLimitAwareRestClient());
        }
    }
}
