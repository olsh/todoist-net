using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;
using Todoist.Net.Tests.Settings;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class LabelsServiceTests
    {
        [Fact]
        [IntegrationFree]
        public void CreateUpdateOrderGetInfoDelete_Success()
        {
            var client = TodoistClientFactory.Create();

            var label = new Label("Test label");
            client.Labels.AddAsync(label).Wait();

            client.Labels.UpdateOrderAsync(new OrderEntry(label.Id, 1)).Wait();

            var labelInfo = client.Labels.GetAsync(label.Id).Result;

            client.Labels.DeleteAsync(labelInfo.Label.Id).Wait();
        }
    }
}
