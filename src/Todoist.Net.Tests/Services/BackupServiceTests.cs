using System.Linq;
using Todoist.Net.Tests.Constants;
using Todoist.Net.Tests.Settings;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class BackupServiceTests
    {
        [Fact]
        [Trait(TraitConstants.Category, TraitConstants.Integration)]
        public void GetBackups_Success()
        {
            var client = new TodoistClient(SettingsProvider.GetToken());

            var backups = client.Backups.GetAsync().Result;

            Assert.True(backups.Count() > 0);
        }
    }
}
