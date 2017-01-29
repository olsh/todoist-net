using Todoist.Net.Tests.Settings;

namespace Todoist.Net.Tests.Helpers
{
    public static class ClientFactory
    {
        public static TodoistClient Create()
        {
            return new TodoistClient(SettingsProvider.GetToken());
        }
    }
}
