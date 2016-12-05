using Todoist.Net.Tests.Settings;

namespace Todoist.Net.Tests.Helpers
{
    public class ClientFactory
    {
        public static TodoistClient Create()
        {
            return new TodoistClient(SettingsProvider.GetToken());
        }
    }
}