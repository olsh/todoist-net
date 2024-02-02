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
    public class SharingServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public SharingServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void ShareProjectGetCollaboratorAndUnshare_NewUser_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var projectId = client.Projects.AddAsync(new Project(Guid.NewGuid().ToString())).Result;

            var email = "you@example.com";
            client.Sharing.ShareProjectAsync(projectId, email).Wait();

            var collaborators = client.Sharing.GetCollaboratorsAsync().Result;
            Assert.Contains(collaborators, c => c.Email == email);

            var collaboratorId = collaborators.First(c => c.Email == email).Id;

            var collaboratorStates = client.Sharing.GetCollaboratorStatesAsync().Result;
            Assert.Contains(collaboratorStates, c => c.UserId == collaboratorId && c.ProjectId == projectId);

            var collaboratorStatus = collaboratorStates.First(c => c.UserId == collaboratorId && c.ProjectId == projectId).State;

            Assert.Equal(CollaboratorStatus.Invited, collaboratorStatus);

            client.Sharing.DeleteCollaboratorAsync(projectId, email).Wait();

            client.Projects.DeleteAsync(projectId).Wait();
        }
    }
}
