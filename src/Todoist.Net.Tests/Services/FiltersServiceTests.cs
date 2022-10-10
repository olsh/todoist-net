using System;
using System.Linq;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
    public class FiltersServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public FiltersServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void GetFilterInfo_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var filters = client.Filters.GetAsync().Result;

            Assert.True(filters.Any());

            var result = client.Filters.GetAsync(filters.First().Id).Result;

            Assert.True(result != null);
        }

        [Fact]
        public void CreateUpdateDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var filter = new Filter(Guid.NewGuid().ToString(), "today");
            client.Filters.AddAsync(filter).Wait();

            var filters = client.Filters.GetAsync().Result;

            Assert.Contains(filters, f => f.Name == filter.Name);

            filter.Query = "test";
            client.Filters.UpdateAsync(filter)
                .Wait();
            var filterOrder = 2;
            client.Filters.UpdateOrderAsync(new OrderEntry(filter.Id, filterOrder)).Wait();

            var filterInfo = client.Filters.GetAsync(filter.Id).Result;
            Assert.Equal(filter.Query, filterInfo.Filter.Query);
            Assert.Equal(filterOrder, filterInfo.Filter.ItemOrder);

            client.Filters.DeleteAsync(filter.Id).Wait();
        }
    }
}
