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
    public class SharingServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public SharingServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task ShareProjectGetCollaboratorAndUnshare_NewUser_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var projectId = await client.Projects.AddAsync(new Project(Guid.NewGuid().ToString()));
            try
            {
                var email = "you@example.com";
                await client.Sharing.ShareProjectAsync(projectId, email);
                try
                {
                    var collaborators = await client.Sharing.GetCollaboratorsAsync();
                    Assert.Contains(collaborators, c => c.Email == email);

                    var collaboratorId = collaborators.First(c => c.Email == email).Id;

                    var collaboratorStates = await client.Sharing.GetCollaboratorStatesAsync();
                    Assert.Contains(collaboratorStates, c => c.UserId == collaboratorId && c.ProjectId == projectId);

                    var collaboratorStatus = collaboratorStates.First(c => c.UserId == collaboratorId && c.ProjectId == projectId).State;

                    Assert.Equal(CollaboratorStatus.Invited, collaboratorStatus);
                }
                finally
                {
                    await client.Sharing.DeleteCollaboratorAsync(projectId, email);
                }
            }
            finally
            {
                await client.Projects.DeleteAsync(projectId);
            }
        }
    }
}
