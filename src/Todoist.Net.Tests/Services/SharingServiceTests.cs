using System;

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
        public void ShareProjectAndUnshare_NewUser_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var projectId = client.Projects.AddAsync(new Project(Guid.NewGuid().ToString())).Result;

            var email = "you@example.com";
            client.Sharing.ShareProjectAsync(projectId, email).Wait();

            client.Sharing.DeleteCollaboratorAsync(projectId, email).Wait();

            client.Projects.DeleteAsync(projectId).Wait();
        }
    }
}
