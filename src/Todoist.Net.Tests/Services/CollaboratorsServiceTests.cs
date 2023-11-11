using System.Linq;
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
        public void GetCollaborators_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var collaborators = client.Collaborators.GetAsync().Result;

            Assert.True(collaborators.Any());
        }

        [Fact]
        public void GetCollaboratorStates_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var collaboratorStates = client.Collaborators.GetStatesAsync().Result;

            Assert.True(collaboratorStates.Any());
        }
    }
}
