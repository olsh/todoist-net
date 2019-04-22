using System.Linq;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    [IntegrationPremium]
    public class ActivityServiceTests
    {
        [Fact]
        public void GetActivity_HasEntries()
        {
            var client = TodoistClientFactory.Create();

            var logEntries = client.Activity.GetAsync(new LogFilter() { Limit = 50 }).Result.Events;

            Assert.NotEmpty(logEntries);
        }

        [Fact]
        public void GetActivityWithEventObjectFilter_HasEntries()
        {
            var client = TodoistClientFactory.Create();

            var logFilter = new LogFilter();
            logFilter.ObjectEventTypes.Add(new ObjectEventTypes() { ObjectType = "project" });

            var logEntries = client.Activity.GetAsync(logFilter).Result.Events;

            Assert.NotEmpty(logEntries);
        }
    }
}
