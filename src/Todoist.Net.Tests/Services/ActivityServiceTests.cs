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
        public void GetActivity_HasEntries()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var logEntries = client.Activity.GetAsync(new LogFilter() { Limit = 50 }).Result.Events;

            Assert.NotEmpty(logEntries);
        }

        [Fact]
        public void GetActivityWithEventObjectFilter_HasEntries()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var logFilter = new LogFilter();
            logFilter.ObjectEventTypes.Add(new ObjectEventTypes() { ObjectType = "project" });

            var logEntries = client.Activity.GetAsync(logFilter).Result.Events;

            Assert.NotEmpty(logEntries);
        }
    }
}
