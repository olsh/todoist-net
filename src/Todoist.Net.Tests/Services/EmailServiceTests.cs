using System;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class EmailServiceTests
    {
        [Fact]
        [IntegrationPremium]
        public void GetOrCreateAsyncDisable_NewProject_Success()
        {
            var client = TodoistClientFactory.Create();

            var projectId = client.Projects.AddAsync(new Project(Guid.NewGuid().ToString())).Result;
            var emailInfo = client.Emails.GetOrCreateAsync(ObjectType.Project, projectId).Result;

            Assert.NotNull(emailInfo);
            Assert.NotNull(emailInfo.Email);

            client.Emails.DisableAsync(ObjectType.Project, projectId).Wait();
            client.Projects.DeleteAsync(projectId).Wait();
        }
    }
}
