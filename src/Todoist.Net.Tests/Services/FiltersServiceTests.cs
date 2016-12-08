using System;
using System.Linq;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;
using Todoist.Net.Tests.Settings;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    [IntegrationPremium]
    public class FiltersServiceTests
    {
        [Fact]
        public void GetFilterInfo_Success()
        {
            var client = CreateClient();

            var filters = client.Filters.GetAsync().Result;

            Assert.True(filters.Count() > 0);

            var result = client.Filters.GetAsync(filters.First().Id).Result;

            Assert.True(result != null);
        }

        [Fact]
        public void CreateDelete_Success()
        {
            var client = CreateClient();

            var filter = new Filter(Guid.NewGuid().ToString(), "today");
            client.Filters.AddAsync(filter).Wait();

            var filters = client.Filters.GetAsync().Result;

            Assert.True(filters.Any(f => f.Name == filter.Name));

            client.Filters.DeleteAsync(filter.Id).Wait();
        }

        private static TodoistClient CreateClient()
        {
            var client = new TodoistClient(SettingsProvider.GetToken());
            return client;
        }
    }
}
