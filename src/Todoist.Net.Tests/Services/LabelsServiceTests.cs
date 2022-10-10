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
        [IntegrationFree]
        public void CreateUpdateOrderGetInfoDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var label = new Label("Test label");
            client.Labels.AddAsync(label).Wait();

            client.Labels.UpdateOrderAsync(new OrderEntry(label.Id, 1)).Wait();

            var labelInfo = client.Labels.GetAsync(label.Id).Result;

            client.Labels.DeleteAsync(labelInfo.Label.Id).Wait();
        }
    }
}
