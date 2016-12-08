using System.Linq;
using Todoist.Net.Tests.Extensions;
using Todoist.Net.Tests.Settings;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class BackupServiceTests
    {
        [Fact]
        [IntegrationFree]
        public void GetBackups_Success()
        {
            var client = new TodoistClient(SettingsProvider.GetToken());

            var backups = client.Backups.GetAsync().Result;

            Assert.True(backups.Count() > 0);
        }
    }
}
