using System;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    public class EmailServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public EmailServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
        public void GetOrCreateAsyncDisable_NewProject_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var projectId = client.Projects.AddAsync(new Project(Guid.NewGuid().ToString())).Result;
            var emailInfo = client.Emails.GetOrCreateAsync(ObjectType.Project, projectId).Result;

            Assert.NotNull(emailInfo);
            Assert.NotNull(emailInfo.Email);

            client.Emails.DisableAsync(ObjectType.Project, projectId).Wait();
            client.Projects.DeleteAsync(projectId).Wait();
        }
    }
}
