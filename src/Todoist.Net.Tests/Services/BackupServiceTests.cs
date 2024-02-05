using System.Linq;
using System.Threading.Tasks;

using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    [Trait(Constants.TraitName, Constants.MfaRequiredTraitValue)]
    public class BackupServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public BackupServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task GetBackups_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var backups = await client.Backups.GetAsync();

            Assert.True(backups.Any());
        }
    }
}
