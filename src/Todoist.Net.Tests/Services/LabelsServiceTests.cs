using System.Threading.Tasks;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    public class LabelsServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public LabelsServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateUpdateOrderGetInfoDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var label = new Label("Test label");
            await client.Labels.AddAsync(label);

            await client.Labels.UpdateOrderAsync(new OrderEntry(label.Id, 1));

            var labelInfo = await client.Labels.GetAsync(label.Id);

            await client.Labels.DeleteAsync(labelInfo.Label.Id);
        }
    }
}
