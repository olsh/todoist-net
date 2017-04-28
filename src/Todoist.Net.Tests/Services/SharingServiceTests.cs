using System;

using Todoist.Net.Models;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class SharingServiceTests
    {
        [Fact]
        public void ShareProjectAndUnshare_NewUser_Success()
        {
            var client = TodoistClientFactory.Create();

            var projectId = client.Projects.AddAsync(new Project(Guid.NewGuid().ToString())).Result;

            var email = "you@example.com";
            client.Sharing.ShareProjectAsync(projectId, email).Wait();

            client.Sharing.DeleteCollaboratorAsync(projectId, email).Wait();

            client.Projects.DeleteAsync(projectId).Wait();
        }
    }
}
