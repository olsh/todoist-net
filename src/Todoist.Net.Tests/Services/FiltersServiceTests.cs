using System;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task GetFilterInfo_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var filters = (await client.Filters.GetAsync()).ToList();

            Assert.True(filters.Count > 0);

            var result = await client.Filters.GetAsync(filters.First().Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateUpdateDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var filter = new Filter(Guid.NewGuid().ToString(), "today");
            await client.Filters.AddAsync(filter);
            try
            {
                var filters = await client.Filters.GetAsync();

                Assert.Contains(filters, f => f.Name == filter.Name);

                filter.Query = "test";
                await client.Filters.UpdateAsync(filter);
                var filterOrder = 2;
                await client.Filters.UpdateOrderAsync(new OrderEntry(filter.Id, filterOrder));

                var filterInfo = await client.Filters.GetAsync(filter.Id);
                Assert.Equal(filter.Query, filterInfo.Filter.Query);
                Assert.Equal(filterOrder, filterInfo.Filter.ItemOrder);
            }
            finally
            {
                await client.Filters.DeleteAsync(filter.Id);
            }
        }
    }
}
