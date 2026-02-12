using System.Threading.Tasks;
using System;
using System.Linq;

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
        public async Task CreateUpdateOrderDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var label = new Label($"Test label {Guid.NewGuid()}");
            var createdLabelId = default(ComplexId);
            try
            {
                await client.Labels.AddAsync(label);

                var labels = (await client.Labels.GetAsync()).ToList();
                var createdLabel = labels.Single(l => l.Name == label.Name);
                createdLabelId = createdLabel.Id;

                await client.Labels.UpdateOrderAsync(new OrderEntry(createdLabelId, 1));
            }
            finally
            {
                if (!createdLabelId.IsEmpty)
                {
                    await client.Labels.DeleteAsync(createdLabelId);
                }
            }
        }
    }
}
