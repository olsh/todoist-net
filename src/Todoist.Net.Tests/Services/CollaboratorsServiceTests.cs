using System.Linq;
using System.Threading.Tasks;

using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
    public class CollaboratorsServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public CollaboratorsServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task GetCollaborators_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var collaborators = await client.Collaborators.GetAsync();

            Assert.True(collaborators.Any());
        }

        [Fact]
        public async Task GetCollaboratorStates_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var collaboratorStates = await client.Collaborators.GetStatesAsync();

            Assert.True(collaboratorStates.Any());
        }
    }
}
