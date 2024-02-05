using System.Threading.Tasks;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
    public class ActivityServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public ActivityServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task TestActivityLogIsNotEmpty()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            const int LogEntriesLimit = 50;
            var logFilter = new LogFilter { Limit = LogEntriesLimit };
            var logEntries = await client.Activity.GetAsync(logFilter);

            Assert.NotEmpty(logEntries.Events);
        }

        [Fact]
        public async Task GetActivityWithEventObjectFilter_HasEntries()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var logFilter = new LogFilter();
            logFilter.ObjectEventTypes.Add(new ObjectEventTypes { ObjectType = "project" });

            var logEntries = await client.Activity.GetAsync(logFilter);

            Assert.NotEmpty(logEntries.Events);
        }
    }
}
