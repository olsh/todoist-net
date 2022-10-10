using System.Linq;
using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
    public class BackupServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public BackupServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void GetBackups_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var backups = client.Backups.GetAsync().Result;

            Assert.True(backups.Any());
        }
    }
}
